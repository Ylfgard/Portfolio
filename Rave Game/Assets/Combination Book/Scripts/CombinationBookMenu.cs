using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CombinationBook
{
    public class CombinationBookMenu : MonoBehaviour, IIDSettable
    {
        [SerializeField] private Mediator _mediator;
        [SerializeField] private Palette _palette; 
        [SerializeField] private GameObject _paintTab;
        [SerializeField] private Transform _paintTabParent;
        [SerializeField] private ToggleGroup _tabToggleGroup;
        [SerializeField] private GameObject _page;
        [SerializeField] private Transform _pageParent;
        private List<BookTab> _bookTabs = new List<BookTab>();
        private List<string> _addedAnimals = new List<string>();
        private CloseMenusCommand _closeCommand = new CloseMenusCommand();
        private AddedAnimalCombinationsCommand _addedAnimalsCommand = new AddedAnimalCombinationsCommand();

        private void Awake()
        {
            _mediator.Subscribe<AddAnimalCombinationCommand>(AddAnimalCombinations);
            _mediator.Subscribe<PaintCellChangedCommand>(UnblockPaintTab);
            foreach(PaintCell paintCell in _palette.PaintCells)
                SpawnPaintTab(paintCell);
        }

        public void SetID(string id)
        {
            _closeCommand.IDs.Add(id);
        }

        public void CloseMenu()
        {
            _mediator.Publish(_closeCommand);
        }

        private void SpawnPaintTab(PaintCell paintCell)
        {
            PaintTab paintTab = Instantiate(_paintTab, _paintTabParent.position, Quaternion.identity).GetComponent<PaintTab>();
            Page page = Instantiate(_page, _pageParent.position, Quaternion.identity).GetComponent<Page>();
            paintTab.Initialize(_paintTabParent, _tabToggleGroup, page, paintCell.Paint);
            page.Initialize(_pageParent);
            BookTab bookTab = new BookTab(paintTab, page, paintCell.Name);
            _bookTabs.Add(bookTab);
        }

        private void UnblockPaintTab(PaintCellChangedCommand callback)
        {
            foreach(PaintCell paintCell in callback.PaintCells)
                foreach(BookTab bookTab in _bookTabs)
                    if(bookTab.PaintName == paintCell.Name)
                        bookTab.PaintTab.MakeInterractable();
        }

        private void AddAnimalCombinations(AddAnimalCombinationCommand callback)
        {
            if(_addedAnimals.Contains(callback.AnimalData.Name))
            {
                return;
            }
            else
            {
                foreach(BookTab bookTab in _bookTabs)
                    if(bookTab.PaintName == callback.AnimalData.Behavior.UnblockingPaint.Name)
                    {
                        bookTab.PaintTab.MakeInterractable();
                        bookTab.Page.SpawnAnimalLine(callback.AnimalData);
                        _addedAnimals.Add(callback.AnimalData.Name);
                        _addedAnimalsCommand.AnimalsNames = _addedAnimals;
                        _mediator.Publish(_addedAnimalsCommand);
                    }
            }     
        }
    }

    public class BookTab
    {
        private PaintTab _paintTab;
        private Page _page;
        private string _paintName;

        public PaintTab PaintTab => _paintTab;

        public string PaintName => _paintName;

        public Page Page => _page;

        public BookTab(PaintTab paintTab, Page page, string paintName)
        {
            _paintTab = paintTab;
            _page = page;
            _paintName = paintName;
        }
    }
}
