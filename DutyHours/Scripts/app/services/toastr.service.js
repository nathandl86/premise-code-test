
/*
 * Toastr Service
 * @namespace Factories
 * @description Factory for application noficiations via toastr
 */
(function (angular, toastr) {
    'use strict';

    angular.module('dutyHoursApp').factory('toastrService', toastrService);

    /*
     * @description Service wrapper for toastr interaction from angular 
     */
    function toastrService() {
        toastr.options = {
            "closeButton": false,
            "newestOnTop": false,
            "positionClass": "toast-bottom-full-width",
            "preventDuplicates": true,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }

        var service = {
            success: success,
            error: error
        }

        return service;

        /*
         * @name success
         * @description Hanldes success notifications 
         */
        function success(text, header) {
            toastr.success(text, header || "Success");
        }

        /*
         * @name error
         * @description Handles error notifications
         */
        function error(text, header) {
            toastr.error(text, header || "Error")
        }

    }

})(window.angular, window.toastr);