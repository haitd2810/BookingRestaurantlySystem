document.addEventListener('DOMContentLoaded', function () {
    let totalPrice = 0;

    const priceElements = document.querySelectorAll('.menu-item .menu-content span');

    priceElements.forEach(function (priceElement) {

        const priceText = priceElement.textContent.replace('$', '');
        const price = parseFloat(priceText);

        if (!isNaN(price)) {
            totalPrice += price;
        }
    });

    const totalPriceElement = document.getElementById('total-price');
    totalPriceElement.textContent = `$${totalPrice.toFixed(2)}`;
});