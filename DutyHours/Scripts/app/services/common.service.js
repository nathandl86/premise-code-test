/*
 * Common Service
 * @namespace Services
 * @description Service housing common functionality needed throughtout app
 */
(function (angular) {
    'use strict';

    commonService.$inject = ['baseApiUri', 'toastrService', 'loggerService'];
    angular.module('dutyHoursApp').service('commonService', commonService);

    /*
     * @description Service wrapper for commonly need services and properties
     */
    function commonService(baseApiUri, toastrService, loggerService) {
        this.notifier = toastrService;
        this.logger = loggerService;
        this.baseApiUri = baseApiUri;
        this.errorHeader = errorHeader;

        function errorHeader(err) {
            if (angular.isDefined(err) && err !== null) {
                if (angular.isDefined(err.status) && err.status
                    && angular.isDefined(err.statusText) && err.StatusText)
                    return err.status + ' | ' + err.statusText;
            }
            return "Unspecified Error";
        }
    }

})(window.angular);