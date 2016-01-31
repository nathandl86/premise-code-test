
/*
 * Institution Service
 * @namespace Factories
 * @description Factory housing api insteractions for institution data
 */
(function (angular) {
    'use strict';

    institutionService.$inject('$http', '$q', 'commonService');
    angular.module('dutyHoursApp').service('commonService', commonService);

    /*
     * @description Service wrapper for institution api interactions
     */
    function institutionService(ngHttp, ngQ, common) {
        var apiPrefix = common.baseApiUri + 'institution/';

        var service = {
            get: get,
            getAll: getAll,
            getResidents: getResidents
        };

        /*
         * @name get
         * @description get a single institution
         */
        function get(id) {
            return ngHttp({
                method: 'GET',
                url: apiPrefix + id
            }).then(function (resp) {
                return resp.data;
            }, function (err) {
                common.logger(err);
                common.notifier.error("Failed to Find Insitution", common.errorHeader(err));
            });
        }

        /*
         * @name getAll
         * @description get all institutions
         */
        function getAll() {
            return ngHttp({
                method: 'GET',
                url: apiPrefix + 'all'
            }).then(function (resp) {
                return resp.data;
            }, function (err) {
                common.logger(err);
                common.notifier.error("Failed to Find Institutions", common.errorHeader(err));
            });
        }

        /*
         * @name getResidents
         * @description get institution residents
         */
        function getResidents(intsitutionId) {
            return ngHttp({
                method: 'GET',
                url: apiPrefix + institutionId + '/residents'
            }).then(function (resp) {
                return resp.data;
            }, function (err) {
                common.logger(err);
                common.notifier.error("Failed to Find Institution Residents", common.errorHeader(err));
            });
        }
    }

})(window.angular);