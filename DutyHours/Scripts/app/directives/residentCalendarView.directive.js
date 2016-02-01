
/*
 * Resident Calendar Page
 * @namespace Directives
 */
(function (angular) {
    'use strict';

    angular.module('dutyHoursApp').directive('residentCalendarView', residentCalendarViewDirective);

    /*
     * @description Directive for housing the resident's shift history
     *      calendar and an entry form for adding/editing shifts
     */
    function residentCalendarViewDirective() {
        var directive = {
            restrict: 'E',
            replace: true,
            scope: false,
            templateUrl: '/Scripts/app/views/residentCalendarView.directive.html',
            controller: 'residentCalendarViewController',
            controllerAs: 'resCalCtrl',
            bindToController: true,
            link: angular.noop
        };

        return directive;
    }

})(window.angular);