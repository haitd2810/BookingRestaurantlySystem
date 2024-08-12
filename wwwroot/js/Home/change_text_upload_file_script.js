document.getElementById('img').addEventListener('change', function () {
    var label = document.querySelector('.custom-file-label');
    if (this.files.length > 0) {
        label.textContent = this.files[0].name;
    }
});