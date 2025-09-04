using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Chemistry;
using MC.Core;
using MC.Core.Unity.UI;
using MC.Core.Unity.Utils;
using MC.Game.Inventory;
using UnityEngine;
using UnityEngine.Events;

namespace MC.Game.Alchemy
{
    //TODO: Extract Animation logic to separate class
    public class AlchemyUI : MonoBehaviour
    {
        private static readonly WaitForSeconds _waitForSeconds0_5 = new(0.5f);
        const int _minProductAmount = 1;

        [Header("Parameters")]
        [SerializeField] ChemicalSpeciesSO[] _possibleProducts = new ChemicalSpeciesSO[] { };
        [SerializeField] int _maxProductAmount = 5;

        [Header("References")]
        [SerializeField] AlchemyReactantsPanelUI _reactantsPanel;
        [SerializeField] AlchemyMixSpeciesUI _productsPanel;
        [SerializeField] InteractableInventoryItemUI _inventoryItemPrefab;
        [SerializeField] Transform _bagTransform;
        [SerializeField] FadingTextUI _fadingTextPrefab;
        [SerializeField] Transform _fadingTextParent;

        [Header("Integration")]
        [SerializeField] UnityEvent _onAwake;
        [SerializeField] UnityEvent _onSuccessfulReaction;
        [SerializeField] UnityEvent _onUnsuccessfulReaction;
        [SerializeField] UnityEvent _onTutorialCompleted;
        [SerializeField] UnityEvent<int> _onGameEnded;

        IInventoryService InventoryService => ServiceLocator.Get<IInventoryService>();
        IAlchemyService AlchemyService => ServiceLocator.Get<IAlchemyService>();

        ComponentPool<InteractableInventoryItemUI> _inventoryItemPool;
        ComponentPool<FadingTextUI> _fadingTextPool;

        private readonly Dictionary<ChemicalSpeciesSO, InteractableInventoryItemUI> _activeBagItems = new();
        readonly System.Random random = new();
        bool _isTutorialCompleted = false;

        //TODO: Extract duplicated code (Fresh and ObjectPool)
        void Awake()
        {
            UIUtils.Fresh(_bagTransform);
            UIUtils.Fresh(_fadingTextParent);

            _inventoryItemPool = new(_inventoryItemPrefab, _bagTransform);
            _fadingTextPool = new(_fadingTextPrefab, _fadingTextParent);

            _possibleProducts ??= new ChemicalSpeciesSO[] { };

            _reactantsPanel.OnRemoveItem = HandleOnRemoveItemFromReactants;

            _onAwake?.Invoke();
        }

        void Start()
        {
            // 1. Populate the bag with inventory items
            PopulateBag();

            // 2. Init mix panel with random product
            SetRandomProduct();
        }

        public void End()
        {
            _reactantsPanel.ReturnAll();

            var inventoryItems = _activeBagItems.Values.Select(ui => ui.InventoryItem).ToArray();

            AlchemyService.End(inventoryItems);
            _onGameEnded?.Invoke(AlchemyService.GetTotalScore());
        }

        public void Exit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }

        public void NewProduct()
        {
            _reactantsPanel.ReturnAll();
            SetRandomProduct();
        }

        public void Mix()
        {
            var reactants = _reactantsPanel.GetReactantPairs();
            var products = _productsPanel.GetProductPairs();

            _reactantsPanel.ClearPanel();

            HandleReactionResult(reactants, products);

            NewProduct();
        }

        public void Reset()
        {
            // 1. Clear any active UI elements
            _reactantsPanel.ReturnAll();
            _productsPanel.ClearPanel();

            // 2. Return all inventory items from the active bag back to the pool
            foreach (var itemUI in _activeBagItems.Values)
            {
                _inventoryItemPool.Release(itemUI);
            }
            _activeBagItems.Clear();

            // 3. Reset game-related services and states
            AlchemyService.Reset();
            _isTutorialCompleted = false;

            // 4. Repopulate the bag and set a new product
            PopulateBag();
            SetRandomProduct();

            Debug.Log("Game state has been reset and a new round has started.");
            _onAwake?.Invoke();
        }

        private void CreateFadingText(string message, Color color)
        {
            var fadingText = _fadingTextPool.Get();
            fadingText.OnFadeComplete = () => _fadingTextPool.Release(fadingText);
            fadingText.Show(message, color);
        }

        private IEnumerator ShowFeedbackWithDelay()
        {
            const string successfulMessage = "Éxito";
            CreateFadingText(successfulMessage, Color.yellow);

            yield return _waitForSeconds0_5;

            if (AlchemyService.CurrentCombo > 1)
            {
                const string comboMessage = "Combo x";
                CreateFadingText($"{comboMessage}{AlchemyService.CurrentCombo}", Color.cyan);
            }
        }

        private void HandleSuccessfulReaction()
        {
            // 1. Add product to inventory
            var inventoryProduct = new InventoryItem(_productsPanel.ResultProduct, 1);
            Debug.Log($"Is balanced. Adding to inventory {inventoryProduct}");

            InventoryService.Add(inventoryProduct);
            AddOrUpdateBagItem(inventoryProduct);

            // 2. Notify service
            AlchemyService.SuccessfulReaction();

            // 3. Show feedback with a delay using a coroutine
            StartCoroutine(ShowFeedbackWithDelay());

            _onSuccessfulReaction?.Invoke();
        }

        private void HandleUnsuccessfulReaction()
        {
            Debug.Log($"Is not balanced.");

            AlchemyService.UnsuccessfulReaction();

            const string unsuccessfulMessage = "Fallo";
            CreateFadingText(unsuccessfulMessage, Color.red);

            _onUnsuccessfulReaction?.Invoke();
        }

        private void HandleReactionResult(MoleculesAmountPair[] reactants, MoleculesAmountPair[] products)
        {
            if (AlchemyService.IsAdjusted(new ChemistryReaction(reactants, products)))
            {
                HandleSuccessfulReaction();
            }
            else
            {
                HandleUnsuccessfulReaction();
            }

            Debug.Log($"Total Score: {AlchemyService.GetTotalScore()}");

            if (!_isTutorialCompleted && AlchemyService.IsTutorialCompleted())
            {
                _isTutorialCompleted = true;
                _onTutorialCompleted?.Invoke();
            }
        }

        private void SetRandomProduct()
        {
            if (_possibleProducts.Length > 0)
            {
                var product = _possibleProducts[random.Next(0, _possibleProducts.Length)];
                _productsPanel.SetResultProduct(product);
                foreach (var monomer in AlchemyService.GetMonomers(product))
                {
                    var inventoryProduct = new InventoryItem(monomer,
                        random.Next(_minProductAmount, _maxProductAmount + 1));
                    _productsPanel.AddMonomer(inventoryProduct);
                }
            }
        }

        private void AddBagItem(InventoryItem inventoryItem)
        {
            var newBagItemUI = _inventoryItemPool.Get();
            newBagItemUI.Setup(inventoryItem);
            _activeBagItems.Add(inventoryItem.Item, newBagItemUI);

            newBagItemUI.SetOnClickAction(() =>
            {
                const int amountOnClick = 1;

                newBagItemUI.Add(-amountOnClick);
                InventoryService.Remove(inventoryItem.Item, amountOnClick);

                _reactantsPanel.AddMonomer(new InventoryItem(inventoryItem.Item, amountOnClick));

                if (newBagItemUI.Amount <= 0)
                {
                    _inventoryItemPool.Release(newBagItemUI);
                    _activeBagItems.Remove(newBagItemUI.Item);
                }
            });
        }

        private void AddOrUpdateBagItem(InventoryItem inventoryItem)
        {
            if (_activeBagItems.TryGetValue(inventoryItem.Item, out var itemUI))
            {
                itemUI.Add(inventoryItem.Amount);
            }
            else
            {
                AddBagItem(inventoryItem);
            }
        }

        private void PopulateBag()
        {
            foreach (var inventoryItem in InventoryService.Inventory)
            {
                AddOrUpdateBagItem(inventoryItem);
            }
        }

        private void HandleOnRemoveItemFromReactants(InventoryItem inventoryItem)
        {
            InventoryService.Add(inventoryItem.Item, inventoryItem.Amount);
            AddOrUpdateBagItem(inventoryItem);
        }
    }
}
