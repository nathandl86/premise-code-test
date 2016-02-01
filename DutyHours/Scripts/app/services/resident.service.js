/*
 * Resident Service
 * @namespace Factories
 * @description Factory wrapping resident api functionality
 */
(function (angular) {
    'use strict';

    residentService.$inject = ['$http', '$q', 'commonService'];
    angular.module('dutyHoursApp').factory('residentService', residentService);

    /*
     * @description Factory wrapping resident api interactions
     */
    function residentService(ngHttp, ngQ, common) {
        var apiPrefix = common.baseApiUri + 'resident/'

        var service = {
            get: get,
            getShifts: getShifts,
            saveShift: saveShift
        }

        return service;
        /*
         * @name get
         * @description gets a specified resident
         */
        function get(residentId) {
            return ngHttp({
                method: 'GET',
                url: apiPrefix + residentId
            }).then(function (resp) {
                return resp.data;
            }, function (err) {
                common.logger.log(err);
                common.notifier.error("Failed to Find Resident", common.errorHeader(err));
            });
        }

        /*
         * @name getShifts
         * @description gets shift history for a resident
         */
        function getShifts(residentId) {
            return ngHttp({
                method: 'GET',
                url: apiPrefix + residentId + '/shifts'
            }).then(function (resp) {
                return resp.data;
            }, function (err) {
                common.logger.log(err);
                common.notifier.error("Failed to Find Resident Shifts", common.errorHeader(err));
            });
        }

        /*
         * @name saveShift
         * @description saves a resident shift
         */
        function saveShift(shift, overrideAck) {
            return ngHttp({
                method: 'POST',
                url: apiPrefix + shift.InstitutionResidentId + '/shift/save',
                data: shift
            }).then(function (resp) {
                common.notifier.success("Shift Saved Successfully!");
            }, function (err) {
                if (err.statusCode === 400) {
                    //TODO: handle shift conflict error and prompt user
                    // if they would like to remove conflicts to save shift
                }
                common.logger.log(err);
                common.notifier.error("Failed to Save Resident Shifts", common.errorHeader(err));
            });
        }
    }

})(window.angular);