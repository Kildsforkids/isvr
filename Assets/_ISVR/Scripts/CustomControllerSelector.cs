﻿using Oculus.Interaction;

namespace ISVR {
    public class CustomControllerSelector : ControllerSelector {

        private bool _lastSelected;
        private bool _wasSelected;

        protected override void Update() {

            bool selected = RequireButtonUsages == ControllerSelectorLogicOperator.All
                ? Controller.IsButtonUsageAllActive(ControllerButtonUsage)
                : Controller.IsButtonUsageAnyActive(ControllerButtonUsage);

            if (selected) {
                if (!_wasSelected) {
                    Select();
                    _wasSelected = true;
                }
            } else {
                _wasSelected = false;
            }
        }

        private void Select() {
            if (_lastSelected) {
                _lastSelected = false;
            } else {
                _lastSelected = true;
            }
            FireSelectAction(_lastSelected);
        }
    }
}