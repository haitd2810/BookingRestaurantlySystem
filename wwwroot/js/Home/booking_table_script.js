document.addEventListener('DOMContentLoaded', function () {
    document.querySelector('.php-email-form').addEventListener('submit', function (event) {

        let phone = document.getElementById('phone').value;
        let date = document.getElementById('date').value;
        let time = document.getElementById('time').value;
        let isValid = true;


        const phonePattern = /^(03|05|08|09)\d{8}$/;
        if (!phonePattern.test(phone)) {
            alert('Please enter phone number at least 10 digits and start by 03 or 05 or 08 or 09!');
            isValid = false;
        }


        let today = new Date();
        today.setHours(0, 0, 0, 0);
        let selectedDate = new Date(date);
        if (selectedDate < today) {
            alert('Please enter Date is current or in the future!');
            isValid = false;
        }


        let currentTime = new Date();
        if (selectedDate.toDateString() === today.toDateString()) {
            let selectedTime = new Date(today.toDateString() + ' ' + time);
            if (selectedTime <= currentTime) {
                alert('Please enter time in the future!');
                isValid = false;
            }
        }


        if (!isValid) {
            event.preventDefault();
        }
    });
});

document.addEventListener('DOMContentLoaded', function () {
    let bookingList = JSON.parse(document.getElementById('bookingListJson').value);

    function formatDate(dateObj) {
        let month = dateObj.Month.toString().padStart(2, '0');
        let day = dateObj.Day.toString().padStart(2, '0');
        return `${dateObj.Year}-${month}-${day}`;
    }

    function formatTime(timeObj) {
        let hour = timeObj.Hour.toString().padStart(2, '0');
        let minute = timeObj.Minute.toString().padStart(2, '0');
        return `${hour}:${minute}`;
    }

    function checkTableAvailability(date, time) {
        let availableTables = ['table01', 'table02', 'table03', 'table04', 'table05', 'table06', 'table07', 'table08'];
        for (let booking of bookingList) {
            let bookingDate = formatDate(booking.DateOrder);
            let bookingTime = formatTime(booking.TimeOrder);
            console.log("date: " + bookingDate + " - Time: " + bookingTime);
            if (bookingDate === date && bookingTime == time) {
                    let tableId = parseInt(booking.TableId);
                    availableTables = availableTables.filter(table => table !== `table${tableId < 10 ? '0' + tableId : tableId}`);
                }
            }
        }
        return availableTables;
    }

    document.getElementById('date').addEventListener('change', function () {
        updateTableSelect();
    });

    document.getElementById('time').addEventListener('change', function () {
        updateTableSelect();
    });

    function updateTableSelect() {
        let date = document.getElementById('date').value;
        let time = document.getElementById('time').value;
        let tableSelect = document.querySelector('.select-table');
        tableSelect.innerHTML = '';

        if (date && time) {
            let availableTables = checkTableAvailability(date, time);
            for (let table of availableTables) {
                let option = document.createElement('option');
                option.value = table.toLowerCase().replace(' ', '');
                option.textContent = table;
                tableSelect.appendChild(option);
            }
        }
    }
});