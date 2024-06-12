document.addEventListener("DOMContentLoaded", function () {
    const tables = document.querySelectorAll('.box .status');
    let freeCount = 0;
    let orderedCount = 0;

    tables.forEach(function (table) {
        if (table.textContent.trim() === 'Free') {
            freeCount++;
        } else if (table.textContent.trim() === 'Ordered') {
            orderedCount++;
        }
    });

    document.getElementById('free-count').textContent = freeCount;
    document.getElementById('ordered-count').textContent = orderedCount;
});