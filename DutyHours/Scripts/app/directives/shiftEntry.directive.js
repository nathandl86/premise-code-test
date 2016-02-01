/*
 * Shift Entry Directive
 * @namespace Directives
 */
(function (angular) {
    'use strict';

    angular.module('dutyHoursApp').directive('shiftEntry', shiftEntryDirective);

    /*
     * @description Directive for entering and persisting shift details
     */
    function shiftEntryDirective() {
        var directive = {
            restrict: 'E',
            replace: true,
            scope: false,
            templateUrl: '/Scripts/app/views/shiftEntry.directive.html',
            controller: 'shiftEntryController',
            controllerAs: 'seCtrl',
            bindToController: true
        };

        return directive
    }

})(window.angular);