

      
            function confirmDelete(userid) {
                Swal.fire({
                    title: 'Are you sure?',
                    // text: 'You won\'t be able to revert this!',
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#d33',
                    cancelButtonColor: '#3085d6',
                    confirmButtonText: 'Yes, delete it!'
                }).then((result) => {
                    if (result.isConfirmed) {
                        // Redirect to delete action
                        window.location.href = `/Admin/delete/${userid}`;
                    }
                });
    }
            function cantdelete() {
                Swal.close();
            Swal.fire({
                title: "Oops...",
            text: "This Supplier has orders cant delete",
               
            });
        }
     
