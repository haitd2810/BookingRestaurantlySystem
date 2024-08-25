$(function () {
    $('input[name="daterange"]').daterangepicker({
        opens: 'left'
    }, function (start, end, label) {
        var now = moment();
        if (start > now || end > now) {
            alert("Please enter date range in the past");
        } else {
            window.location.href = 'StatisticStaff' + '?start=' + start.format('YYYY-MM-DD 00:00:00 A') + '&end=' + end.format('YYYY-MM-DD 11:59:59 A');
        }
    });
});
