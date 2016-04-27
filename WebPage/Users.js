//--- initialize the Google table chart
google.load('visualization', '1', { packages: ['table', 'corechart', 'line'] });

$(document).ready(function () {

    var usersTable = new usersTable('usersDiv');

    usersTable.doit();

    //-----------------------------------------------------------
    // usersTable
    //-----------------------------------------------------------
    function usersTable(divID) {
        this.table = new google.visualization.Table(document.getElementById(divID));
        
        this.doit = function () {
            var url = 'http://herrober.rdeadmin.waters.com:4711/users';
            $.getJSON(url, function (data) {
                console.log(data);
            });            
        }
    }
});
