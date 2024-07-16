document.addEventListener('DOMContentLoaded', function () {
    const feedbackTextarea = document.getElementById('feedback');
    const button = document.getElementById('button-feedback');

    function toggleButtonState() {
        var feedback = feedbackTextarea.value;
        var arrayFeedback = feedback.split(" ");
        if (arrayFeedback.length < 25 || feedback.trim() === "") {
            button.disabled = true;
        } else {
            button.disabled = false;
        }
    }

    toggleButtonState();

    feedbackTextarea.addEventListener('input', toggleButtonState);

    document.getElementById('feedbackForm').addEventListener('submit', function (event) {
        event.preventDefault();

        document.getElementById('thankYouMessage').style.display = 'block';

        setTimeout(function () {
            document.getElementById('thankYouMessage').style.display = 'none';
            event.target.submit();
        }, 5000);
    });
});
