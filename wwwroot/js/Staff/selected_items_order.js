document.addEventListener('DOMContentLoaded', function () {
    const checkboxes = document.querySelectorAll('.form-check-input');
    const selectedItemsContainer = document.getElementById('selected-items-container');

    checkboxes.forEach(checkbox => {
        checkbox.addEventListener('change', function () {
            updateSelectedItems();
        });
    });

    function updateSelectedItems() {
        
        selectedItemsContainer.innerHTML = '';

        
        checkboxes.forEach(checkbox => {
            if (checkbox.checked) {
                
                const menuItem = checkbox.closest('.menu-item').cloneNode(true);
                
                const clonedCheckbox = menuItem.querySelector('.form-check-input');
                clonedCheckbox.parentNode.removeChild(clonedCheckbox);
                
                const quantityControl = document.createElement('div');
                quantityControl.className = 'quantity-control';
                quantityControl.innerHTML = `
                        <button class="btn decrement-btn">-</button>
                        <span class="quantity">1</span>
                        <button class="btn increment-btn">+</button>
                    `;
                menuItem.appendChild(quantityControl);

                selectedItemsContainer.appendChild(menuItem);

                const decrementBtn = menuItem.querySelector('.decrement-btn');
                const incrementBtn = menuItem.querySelector('.increment-btn');
                const quantitySpan = menuItem.querySelector('.quantity');

                decrementBtn.addEventListener('click', function () {
                    let quantity = parseInt(quantitySpan.textContent);
                    if (quantity > 1) {
                        quantitySpan.textContent = --quantity;
                        menuItem.setAttribute('data-quantity', quantity);
                    }
                });

                incrementBtn.addEventListener('click', function () {
                    let quantity = parseInt(quantitySpan.textContent);
                    quantitySpan.textContent = ++quantity;
                    menuItem.setAttribute('data-quantity', quantity);
                });

                const itemName = menuItem.querySelector('.menu-content a').textContent;
                menuItem.setAttribute('data-name', itemName);
                menuItem.setAttribute('data-quantity', 1);
            }
        });
    }
});

function getSelectedItems() {
    const selectedItemsContainer = document.getElementById('selected-items-container');
    const selectedItems = selectedItemsContainer.querySelectorAll('.menu-item');
    const items = [];

    selectedItems.forEach(item => {
        const name = item.getAttribute('data-name');
        const quantity = item.getAttribute('data-quantity');
        items.push({ name, quantity });
    });

    return items;
}

// Example usage
document.getElementById('retrieve-button').addEventListener('click', function () {
    const selectedItems = getSelectedItems();
    console.log(selectedItems);
});