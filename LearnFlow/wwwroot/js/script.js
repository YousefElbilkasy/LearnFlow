$(document).ready(function () {
    $('#searchText').on('input', function () {
        let query = $(this).val();
        if (query.length > 1) {
            $.ajax({
                url: '/Course/GetCourses', 
                type: 'GET',
                data: { search: query },
                success: function (data) {
                    $('#courseList').empty(); 

                    if (data.length === 0) {
                        $('#courseList').append('<li>No Result!!</li>');
                    } else {
                        data.forEach(function (course) {
                            $('#courseList').append(`
                                <li data-id="${course.courseId}">
                                    <img src="${course.image}" alt="${course.title}" />
                                    <div>
                                        <strong>${course.title}</strong>
                                        <span>${course.description.substring(0, 50)}...</span>
                                    </div>
                                </li>
                            `);
                        });
                    }
                },
                error: function () {
                    $('#courseList').empty().append('<li>Error occered while fetching results</li>');
                }
            });
        } else {
            $('#courseList').empty();
        }
    });

   
    $('#courseList').on('click', 'li', function () {
        let courseId = $(this).data('id');
        window.location.href = `/Course/Details/${courseId}`;
    });
});


$(document).ready(function () {
    $('#searchText').on('input', function () {
        let query = $(this).val();
        if (query.length > 1) {
            $.ajax({
                url: '/Course/GetCourses', 
                type: 'GET',
                data: { search: query },
                success: function (data) {
                    $('#courseList').empty(); 

                    if (data.length === 0) {
                        $('#courseList').append('<li>No Result!!</li>');
                    } else {
                        data.forEach(function (course) {
                            $('#courseList').append(`
                                <li data-id="${course.courseId}">
                                    <img src="${course.image}" alt="${course.title}" />
                                    <div>
                                        <strong>${course.title}</strong>
                                        <span>${course.description.substring(0, 50)}...</span>
                                    </div>
                                </li>
                            `);
                        });
                    }
                },
                error: function () {
                    $('#courseList').empty().append('<li>Error occered while fetching results</li>');
                }
            });
        } else {
            $('#courseList').empty();
        }
    });

   
    $('#courseList').on('click', 'li', function () {
        let courseId = $(this).data('id');
        window.location.href = `/Course/Details/${courseId}`;
    });
});

