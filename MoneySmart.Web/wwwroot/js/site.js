///<reference path="../lib/jquery/jquery.js"/>

$(function() {
    console.log('Hello from site.js');
    $('.card a').on('click', function(e) {
        e.preventDefault();
        console.log('clicked the transaction card');
        const id = $(this).data('id');
        $('#TransactionDetailModal .modal-title').text(`Details #${id}`);
        $('#TransactionDetailModal .modal-body').load(`?handler=TransactionDetails&id=${id}`);
        $('#TransactionDetailModal').modal('show');
    });
});