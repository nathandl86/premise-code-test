/*
 * Resident Calendar View Controller
 * @namespace Controllers
 */
(function (angular) {
    'use strict';

    residentCalendarViewController.$inject = ['$http', 'appService', 'residentService'];
    angular.module('dutyHoursApp').controller('residentCalendarViewController', residentCalendarViewController);

    function residentCalendarViewController(ngHttp, appService, residentService) {
        var vm = this;

        //properties
        vm.resident = null;
        vm.institution = null;
        vm.selectedShift = null;
        vm.shifts = [];

        //methods
        vm.getResidentText = getResidentText;

        //watches

        init();
        return;

        //functions
        function init() {
            getInstitution();
            getResident();
        }

        /*
         * @name getResident
         * @descrition Function to get the resident from the app service. Handles promise
         * resolution and will request another in case the selected resident
         * changes.
         */
        function getResident() {
            var res = appService.getResidentPromise();
            if (res.currentValue != null) {
                vm.resident = res.currentValue;
            }

            res.promise.then(residentSet);

            function residentSet(resident) {
                vm.resident = resident;
                getResidentShifts(resident.id);
                res = appService.getResidentPromise();
                res.promise.then(residentSet);
            }
        }

        /*
         * @name getInstitution
         * @description Function to get the institution from the app service. Handles
         *      promise resolution if at the time of request, an institution isn't set
         */
        function getInstitution() {
            var inst = appService.getInstitutionPromise();
            if (inst.currentValue != null) {
                vm.institution = inst.currentValue;
                return;
            }

            inst.promise.then(function (institution) {
                vm.institution = institution;
            });
        }

        /*
         * @name getResidentShifts
         * @description Function to get the resident's shift history
         */
        function getResidentShifts(residentId) {
            residentService.getShifts(residentId)
                .then(function (data) {
                    vm.shifts = data;
                });
        }

        /*
         * @name getResidentText
         * @description Function to get resident text
         */
        function getResidentText() {
            if (vm.resident && angular.isDefined(vm.resident.user) && vm.resident.user) {
                return vm.resident.user.firstName + ' ' + vm.resident.user.lastName;
            }
            return "";
        }
    }

})(window.angular);