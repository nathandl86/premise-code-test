(function (angular) {
    'use strict';

    tmpPageController.$inject = ['$scope', 'institutionService', 'appService'];
    angular.module('dutyHoursApp').controller('tmpPageController', tmpPageController);

    function tmpPageController(ngScope, institutionService, appService) {
        var vm = this;

        vm.institutions = [];
        vm.residents = [];
        vm.institution = null;
        vm.resident = null;

        ngScope.$watch(function () {
            return vm.institution;
        }, function (val) {
            if (val) {
                getResidents(vm.institution.id);
                appService.setInstitution(val);
            }
            else {
                vm.residents = [];
            }
        });

        ngScope.$watch(function () {
            return vm.resident;
        }, function (val) {
            if (val) {
                appService.setResident(val);
            }
        });

        init();
        return;

        //functions
        function init() {
            institutionService.getAll()
                .then(function (data) {
                    if (angular.isDefined(data) && data && data.length > 0) {
                        vm.institutions = data;
                    }
                    else {
                        vm.institutions = [];
                    }
                });
        }

        function getResidents(institutionId) {
            institutionService.getResidents(institutionId)
                .then(function (data) {
                    if (angular.isDefined(data) && data && data.length > 0) {
                        vm.residents = data;                        
                    }
                    else {
                        vm.residents = [];
                    }
                });
        }
    }

})(window.angular);