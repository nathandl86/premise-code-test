/*
 * App Service
 * @namespace Factory
 * @description Factory for communicating application-level state changes
 */
(function (angular) {
    'use strict';

    appService.$inject = ['$q'];
    angular.module('dutyHoursApp').factory('appService', appService);

    /*
     * @description Factory to store and communicate changes to applicaton level state changes
     */
    function appService(ngQ) {
        var resident = null,
            institution = null,
            residentPromises = [],
            institutionPromises = [];

        var service = {
            getResidentPromise: getResidentPromise,
            getInstitutionPromise: getInstitutionPromise,
            setInstitution: setInstitution,
            setResident: setResident
        }

        return service;

        /*
         * @name getResidentPromise
         * @description Service function returning the current resident and a promise 
         *          for the next change to resident.
         */
        function getResidentPromise() {
            var deferred = ngQ.defer();
            residentPromises.push(deferred);
            return {
                currentValue: resident,
                promise: deferred.promise
            };
        }

        /*
         * @name getInstitutionPromise
         * @description Service function returning the current institution and 
         *          and a promise for the next change
         */
        function getInstitutionPromise() {
            var deferred = ngQ.defer();
            institutionPromises.push(deferred);
            return {
                currentValue: institution,
                promise: deferred.promise
            };
        }

        /*
         * @name setInstitution
         * @description Service function to store the institution and resolve
         *      promise waiting for the change
         */
        function setInstitution(val) {
            if (institution === val) return;
            institution = val;
            institutionPromises.forEach(function (d) {
                d.resolve(institution);
            });
            institutionPromises = [];
        }

        /*
         * @name setResident
         * @description Service function to store the resident and resolve
         *      promises waiting for the change
         */
        function setResident(val) {
            if (resident === val) return;
            resident = val;
            residentPromises.forEach(function (d) {
                d.resolve(resident);
            });
            residentPromises = [];
        }
    }

})(window.angular);