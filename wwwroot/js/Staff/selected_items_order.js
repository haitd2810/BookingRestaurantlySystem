document.addEventListener('DOMContentLoaded', function () {
    const checkboxes = document.querySelectorAll('.form-check-input');
    const selectedItemsContainer = document.getElementById('selected-items-container');

    // Object to store selected items and their quantities
    const selectedItems = {};

    checkboxes.forEach(checkbox => {
        checkbox.addEventListener('change', function () {
            updateSelectedItems();
        });
    });

    function updateSelectedItems() {
        selectedItemsContainer.innerHTML = '';

        checkboxes.forEach(checkbox => {
            if (checkbox.checked) {
                const menuItem = checkbox.closest('.menu-item');
                const itemName = menuItem.querySelector('.menu-content a').textContent;
                const itemId = menuItem.querySelector('input[name="mealID"]').value; // Get the ID of the item

                // If the item is not already selected, add it with a default quantity of 1
                if (!selectedItems[itemName]) {
                    selectedItems[itemName] = {
                        id: itemId, 
                        element: menuItem.cloneNode(true),
                        quantity: 1
                    };
                }

                // Create a clone of the menu item to display in the selected items container
                const clonedItem = selectedItems[itemName].element.cloneNode(true);
                const clonedCheckbox = clonedItem.querySelector('.form-check-input');
                if (clonedCheckbox) clonedCheckbox.parentNode.removeChild(clonedCheckbox);

                // Add quantity controls to the cloned item
                const quantityControl = document.createElement('div');
                quantityControl.className = 'quantity-control';
                quantityControl.innerHTML = `
                    <button class="decrement-btn">-</button>
                    <span class="quantity">${selectedItems[itemName].quantity}</span>
                    <button class="btn increment-btn">+</button>
                `;
                clonedItem.appendChild(quantityControl);
                selectedItemsContainer.appendChild(clonedItem);

                const decrementBtn = clonedItem.querySelector('.decrement-btn');
                const incrementBtn = clonedItem.querySelector('.increment-btn');
                const quantitySpan = clonedItem.querySelector('.quantity');

                decrementBtn.addEventListener('click', function () {
                    let quantity = selectedItems[itemName].quantity;
                    if (quantity > 1) {
                        quantitySpan.textContent = --quantity;
                        selectedItems[itemName].quantity = quantity;
                    }
                });

                incrementBtn.addEventListener('click', function () {
                    let quantity = selectedItems[itemName].quantity;
                    quantitySpan.textContent = ++quantity;
                    selectedItems[itemName].quantity = quantity;
                });
            }
        });

        // Remove items from the selectedItems object if they are unchecked
        Object.keys(selectedItems).forEach(itemName => {
            if (![...checkboxes].find(checkbox => checkbox.checked && checkbox.closest('.menu-item').querySelector('.menu-content a').textContent === itemName)) {
                delete selectedItems[itemName];
            }
        });
    }

    function getSelectedItems() {
        const items = [];
        Object.keys(selectedItems).forEach(itemName => {
            items.push({
                id: selectedItems[itemName].id, // Include the item ID
                name: itemName,
                quantity: selectedItems[itemName].quantity
            });
        });
        return items;
    }

    // Example usage
    document.getElementById('retrieve-button').addEventListener('click', function () {
        const selectedItemsList = getSelectedItems();
    });
});
