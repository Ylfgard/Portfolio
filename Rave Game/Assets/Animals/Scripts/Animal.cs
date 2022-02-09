using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animals
{
    public class Animal : MonoBehaviour
    {
        [SerializeField] private AnimalBehavior _animalBehavior;
        private Mediator _mediator;
        private Palette _palette;
        private int _dayPassed;
        private bool _dontBringIncome;
        private MakePaintPriceNormalCommand command = new MakePaintPriceNormalCommand();

        public void Init(Palette palette, Mediator mediator)
        {
            _palette = palette;
            _mediator = mediator;
            _dayPassed = 0;
            _palette.UnblockPaint(_animalBehavior.UnblockingPaint);
            command.Paint = _animalBehavior.UnblockingPaint;
            command.Unblock = true;
            _dontBringIncome = false;
            _mediator.Subscribe<TreeLevelStagesCompleteCommand>(StopBringIncome);
            _mediator.Publish(command);
        }

        public void EndDay()
        {
            _dayPassed++;
            if(_dayPassed == _animalBehavior.Period)
                BringIncome();
        }

        private void StopBringIncome(TreeLevelStagesCompleteCommand callback)
        {
            _dontBringIncome = true;
        }

        private void BringIncome()
        {
            if(_dontBringIncome == false)
            {
                if(_animalBehavior.BringEssence == false)
                {
                    _palette.ChangePaintCount(_animalBehavior.Paint.Name, _animalBehavior.Income);
                }
                else
                {
                    GetEssenceCommand command = new GetEssenceCommand();
                    command.Count = _animalBehavior.Income;
                    _mediator.Publish(command);
                }
                _dayPassed = 0;
            }
        }
        
        public void Despawn()
        {
            command.Unblock = false;
            _mediator.Publish(command);
            Destroy(gameObject);
        }
    }
}

