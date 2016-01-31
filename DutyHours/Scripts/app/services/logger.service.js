/*
 * Logger Service
 * @namespace Factories
 * @description Factory for logging.
 */
(function (angular) {
    'use strict';

    angular.module('dutyHoursApp').factory('loggerService', loggerService);

    /*
     * @description Service wrapper for logging to the browser console
     */
    function loggerService() {
        var service = {
            log: log
        };

        return service;

        /*
         * @name log
         * @description Handles logging to brower console
         */
        function log(data) {
            console.log(data);
        }
    }

})(window.angular);