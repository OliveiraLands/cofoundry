<<<<<<< HEAD
﻿angular.module('cms.shared').factory('shared.localStorage', ['shared.serviceBase', function (serviceBase) {
    var service = {},
        localStorageServiceBase = serviceBase + 'localStorage';

    service.setValue = function (key, value) {
        localStorage.setItem(key, value);
    }

    service.getValue = function (key) {
        var value = localStorage.getItem(key);
        return value;
    }

    return service;
=======
﻿angular.module('cms.shared').factory('shared.localStorage', ['shared.serviceBase', function (serviceBase) {
    var service = {},
        localStorageServiceBase = serviceBase + 'localStorage';

    service.setValue = function (key, value) {
        localStorage.setItem(key, value);
    }

    service.getValue = function (key) {
        var value = localStorage.getItem(key);
        return value;
    }

    return service;
>>>>>>> 6ecdeb969200643b332b1c86e2aba97ab0ff9ce6
}]);