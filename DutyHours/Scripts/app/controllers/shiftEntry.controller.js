/*
 * Shift Entry Controller
 * @namespace Directives
 */
(function (angular) {
    'use strict';

    shiftEntryController.$inject = ['commonService', 'residentService'];
    angular.module('dutyHoursApp').controller('shiftEntryController', shiftEntryController);

    /*
     * @description Controller for the shift entry directive
     */
    function shiftEntryController(common, residentService) {
        var vm = this;
        var min = new Date();
        var now = new Date();
        min.setDate(min.getDate() - 31);

        //Properties
        vm.startDate = now;
        vm.startTime = now;
        vm.endDate = null;
        vm.endTime = null;

        vm.dataFormat = "MM/dd/yyyy";
        vm.minDate = min;
        vm.maxDate = new Date();
        vm.startDatePickerOpen = false;
        vm.endDatePickerOpen = false;
        vm.dateOptions = {
            formatYear: 'yyyy',
            startingDay: 1
        };
        vm.hourStep = 1;
        vm.minStep = 5;
        vm.timeMeridian = true;

        //methods
        vm.openStartDate = openStartDate;
        vm.openEndDate = openEndDate;
        vm.saveClicked = saveClicked;
        vm.saveEnabled = saveEnabled;

        init();
        return;

        function init() {

        }

        function openStartDate() {
            vm.startDatePickerOpen = true;
        }

        function openEndDate() {
            vm.endDatePickerOpen = true;
        }

        function saveEnabled() {
            if (!angular.isDefined(vm.resident) || !vm.resident) {
                return false;
            }

            if (!angular.isDefined(vm.startDate) || !vm.startDate ||
                !angular.isDefined(vm.startTime) || !vm.startTime) {
                return false;
            }

            return true;
        }

        function saveClicked() {
            var startHour = vm.startTime.getHours();
            var startMin = vm.startTime.getMinutes();
            var endHour = vm.endTime === null ? null : vm.endTime.getHours();
            var endMin = vm.endTime === null ? null : vm.endTime.getMinutes();

            vm.startDate.setHours(startHour);
            vm.startDate.setMinutes(startMin);

            if (vm.endDate && endHour && endMin) {
                vm.endDate.setHours(endHour);
                vm.endDate.setMinutes(endMin);
            }

            if (vm.startDate >= vm.endDate) {
                common.notifier.error("The End Date & Time must be after the Start Date & Time", "Invalid Date and/or Time")
                return;
            }

            if (vm.shift === null) {
                vm.shift = {
                    institutionResidentId: vm.resident.id,
                    entryDateTime: new Date()
                };
            }
            vm.shift.startDateTime = vm.startDate;
            vm.shift.endDateTime = vm.endDate;

            residentService.saveShift(vm.shift)
                .then(function (data) {
                    vm.shifts = data;
                });
        }
    }

})(window.angular);