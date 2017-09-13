function PreformGetCustomers(pageNumber, onComplete, onFailed) {
    var self = this;
    var apiUrl = "https://backend-challenge-winter-2017.herokuapp.com/customers.json?page=" + pageNumber;
    //alert(pageNumber + "|" + apiUrl);
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (this.readyState !== 4) return;
        if (this.status === 200) {
            var parsedResponse = JSON.parse(this.responseText);
            //alert("Data Loaded: " + parsedResponse);
            if (onComplete !== null) {
                onComplete(parsedResponse);
            }    
        }
        else {
            if (onFailed !== null) {
                onFailed(this.responseText);
            }
        }
    };
    xhr.open("GET", apiUrl, true);
    xhr.setRequestHeader('Accept', 'application/json');
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send();
};

var apiCompleted = function(response) {
    alert(response);
};
var apiFailed = function(response) {
    alert(repsonse);
};

PreformGetCustomers(1, apiCompleted, apiFailed);