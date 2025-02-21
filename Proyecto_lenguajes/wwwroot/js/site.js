const modals = document.querySelectorAll('.custom-modal');
const closeButtons = document.querySelectorAll('.custom-modal-close');
const navigationBar = document.getElementById('navBar');
let newsList = [];
let currentIndex = 0;
const newsPerPage = 3;

$(document).ready(function () {
    GetCoursesByCycle(1);
    loadAllNewsDescending();
    LoadProfessor();

    //Required for courses and course comments
    const month = new Date().getMonth() + 1;

    let fillHtml = "";

    if (month >= 2 && month <= 7) {
        fillHtml = `
            <option value="I">I</option>
            <option value="III">III</option>
            <option value="V">V</option>
            <option value="VII">VII</option>
        `;
    } else if (month >= 8 && month <= 12) {
        fillHtml = `
            <option value="II">II</option>
            <option value="IV">IV</option>
            <option value="VI">VI</option>
            <option value="VIII">VIII</option>
        `;
    } else {
        fillHtml = `
            <option value="I">I</option>
            <option value="II">II</option>
            <option value="III">III</option>
            <option value="IV">IV</option>
            <option value="V">V</option>
            <option value="VI">VI</option>
            <option value="VII">VII</option>
            <option value="VIII">VIII</option>
        `;
    }
    //Required for courses and course comments
    $("#cicles").html(fillHtml);

    //Required for courses and course comments
    $("#cicles").change(function () {
        let romanCycle = $(this).val();
        let cycle = convertRomanToInt(romanCycle);
        GetCoursesByCycle(cycle);
    });

    $("#prevBtn").click(function () {
        if (currentIndex > 0) {
            currentIndex -= newsPerPage;
            updateNewsDisplay();
        }
    });

    $("#nextBtn").click(function () {
        if (currentIndex + newsPerPage < newsList.length) {
            currentIndex += newsPerPage;
            updateNewsDisplay();
        }
    });
});
function LoadProfessor() {
    $.ajax({
        url: "/Professor/Get",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (professorList) {
            let professorSelect = $("#professorSelect"); // Asegúrate de tener un <select> con este ID
            professorSelect.empty(); // Limpiar opciones antes de agregar nuevas

            $.each(professorList, function (index, professor) {
                let option = `<option value="${professor.id}">${professor.name} (${professor.id})</option>`;
                professorSelect.append(option);
            });
        },
        error: function () {
            alert("Error fetching professors");
        }
    });
}



function PostApplicationConsultation() {



    let appointmentType = $("#appointmentType").val(); // Obtener el valor del select

    if (appointmentType === "1") { // Solo ejecuta si el select está en "0"

        let professorSelect = $("#professorSelect option:selected"); // Obtiene la opción seleccionada
        let professorId = professorSelect.val(); // Obtiene el ID del profesor
        let professorName = professorSelect.text().replace(/\s*\(\d+\)$/, ''); // Extrae el nombre sin el ID

        let applicationData = {
            Text: $("#txtConsult").val(), // Asegurar que este input exista en el HTML
            Student: {
                Id: $("#studentID").text(), // Asegurar que el campo de estudiante exista
                Name: $("#studentName").text()
            },

            Professor: {
                Id: professorId,
                Name: professorName
            }
        };

        $.ajax({
            url: "/PrivateConsultation/Post",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(applicationData),
            dataType: "json",
            success: function (response) {
                alert("Consulta enviada exitosamente");
            },
            error: function (error) {
                alert("Error al enviar la consulta");
            }
        });
    } else {
        alert("Seleccione una opción válida para enviar la consulta.");
    }

}

//Required for courses and course comments
function convertRomanToInt(roman) {
    const romanMap = { "I": 1, "II": 2, "III": 3, "IV": 4, "V": 5, "VI": 6, "VII": 7, "VIII": 8 };
    return romanMap[roman] || 0; 
}
function AuthenticateStudent() {
    // Construir el objeto student con los valores del formulario
    let student = {
        id: $("#lId").val(),
        password: $("#lPassword").val()
    };

    // Enviar los datos al backend con AJAX
    $.ajax({
        url: "/Student/Authenticate",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(student), // Convertir objeto a JSON
        dataType: "json",
        success: function (response) {
            GetStudentData(student.id);
            alert("Authentication successful!");

            // Vaciar los campos de entrada
            $("#lId").val('');
            $("#lPassword").val('');
        },
        error: function (xhr) {
            switch (xhr.status) {
                case 401:
                    alert("Incorrect password.");
                    break;
                case 404:
                    alert("User does not exist.");
                    break;
                case 403:
                    alert("User is not active.");
                    break;
                default:
                    alert("Error authenticating. Check your credentials and try again.");
                    break;
            }
        }
    });
}
function GetStudentData() {
    $.ajax({
        url: "/Student/GetStudentDataFromSession",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (student) {
            if (student) {
                $("#studentName").text(student.name + " " + student.lastName);
                $("#studentID").text(student.id);

                $("#pName").val(student.name);
                $("#pSname").val(student.lastName);
                $("#pMail").val(student.email);

                if (student.photo) {
                    $("#profileModal img").attr("src", `data:image/png;base64,${student.photo}`);
                } else {
                    $("#profileModal img").attr("src", "/images/default.jpg"); // Imagen por defecto
                }
            } else {
                alert("No se encontraron datos del estudiante.");
            }
        },
        error: function () {
            alert("Error al obtener los datos del estudiante.");
        }
    });
}
function PostStudent() {
    // Construir el objeto student con los valores del formulario
    let student = {
        id: $("#rId").val(),
        name: $("#rName").val(),
        lastName: $("#rSname").val(),
        email: $("#rMail").val(),
        password: $("#rPassword").val(),
        likings: $("input[name='interesesR']:checked").val()
    };

  

   

    // Enviar los datos al backend con AJAX
    $.ajax({
        url: "/Student/Post",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(student), // Convertir objeto a JSON
        dataType: "json",
        success: function (response) {
            alert("Student registered successfully!");
        },
        error: function () {
            alert("Error registering student. Try again.");
        }
    });
}
function GetNewsById(idNot) {
    $.ajax({
        url: "/BreakingNew/Get",
        type: "GET",
        data: { idNot: idNot },
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result) {
                addNewsComponent(result);
            } else {
                alert("No se encontró la noticia.");
            }
        },
        error: function (errorMessage) {
            alert("Error al obtener la noticia.");
        }
    });
}

//Required for courses and course comments
function GetCommentCourseById(id) {
    $.ajax({
        url: "/CommentNew/Get",  // Ruta al controlador y método
        type: "GET",
        data: { id: id },  // Parámetro id que se pasa al método
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result && result.length > 0) {
                // Aquí agregamos los comentarios al contenedor
                addCommentsToContainer(result);
            } else {
                alert("No se encontraron comentarios.");
            }
        },
        error: function (errorMessage) {
            alert("Error al obtener los comentarios.");
        }
    });
}
function addCommentsToContainer(comments) {
    // Limpiar el contenedor antes de agregar los nuevos comentarios
    $(".news-comment-loader").empty();

    // Recorrer todos los comentarios y agregarlos al contenedor
    comments.forEach(function (comment) {
        var commentHtml = `
            <div class="comment col-xs-12 col-sm-9 col-lg-10">
                <h4 class="media-heading">${comment.name1}</h4>
                <p>${comment.content}</p>
            </div>
        `;
        // Agregar el comentario al contenedor
        $(".news-comment-loader").append(commentHtml);
    });
}

//Required for courses and course comments
function GetCoursesByCycle(cycle) {
    $.ajax({
        url: "/Course/GetByCycle",
        type: "GET",
        data: { cycle: cycle },
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (courses) {
            let tableBody = $("#coursesTable tbody");
            tableBody.empty(); // Limpiar la tabla

            if (courses.length > 0) {
                $.each(courses, function (index, course) {
                    let row = `<tr>
                                <td>${course.acronym}</td>
                                <td>${course.name}</td>
                                <td><a href="#about" onclick="GetCourseByAcronym('${course.acronym}')">>></a></td>
                              </tr>`;
                    tableBody.append(row);
                });

                // Llamar automáticamente a GetCourseByAcronym para el primer curso
                GetCourseByAcronym(courses[0].acronym); // Primer elemento en la lista
            } else {
                // Si no hay cursos, mostramos un mensaje
                tableBody.append(`<tr><td colspan="3" class="text-center">No courses found</td></tr>`);
            }
        },
        error: function () {
            alert("Error retrieving courses.");
        }
    });
}

//Required for courses and course comments
function GetCommentsByCourseId(courseId) {
    $.ajax({
        url: "/CommentCourse/Get",
        type: "GET",
        data: { id: courseId },
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (comments) {
            loadComentarios(comments);
        },
        error: function (errorMessage) {
            console.error("Error fetching comments:", errorMessage);
        }
    });
}

//Required for courses and course comments
function loadComentarios(comments) {

    GetStudentPhotoById();

    $(".course-comment-loader").empty(); // Limpiar comentarios anteriores

    comments.forEach(comment => {
        var uniqueId = `img-${Math.random().toString(36).substr(2, 9)}`;

        var commentHtml = `
            <div class="media">
                <div class="col-sm-3 col-lg-2 hidden-xs">
                    <img id="${uniqueId}" class="comment-media-object" src="/images/defaultpfp.jpg" alt="">
                </div>
                <div class="comment col-xs-12 col-sm-9 col-lg-10">
                    <h4 class="media-heading">${comment.idUser}</h4>
                    <p>${comment.date}</p>
                    <p>${comment.content}</p>
                </div>
            </div>
        `;
        $(".course-comment-loader").append(commentHtml);

        GetPhoto(comment.idUser, "Professor")
            .then(photo => {
                if (!photo) {
                    return GetPhoto(comment.idUser, "Student");
                }
                return photo;
            })
            .then(photo => {
                if (photo) {
                    document.getElementById(uniqueId).src = `data:image/png;base64,${photo}`;
                }
            })
            .catch(() => {
                alert("Error to take photo.");
            });
    });
}

//Required for courses and course comments
function GetPhoto(id, type) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: `/${type}/Get`,
            type: "GET",
            data: { id: id },
            contentType: "application/json;charset=utf-8",
            dataType: "json"
        })
            .done(result => {
                if (result && result.photo) {
                    resolve(result.photo);
                } else {
                    resolve(null);
                }
            })
            .fail(() => {
                reject();
            });
    });
}

function GetStudentPhotoById() {
    $.ajax({
        url: "/Student/GetStudentDataFromSession",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (student) {
            if (student) {
                const imgElement = document.getElementById('imageUser');
                base64ToImage(student.photo, imgElement);
            } else {
                alert("No se encontraron datos del estudiante.");
            }
        },
        error: function () {
            alert("Error al obtener los datos del estudiante.");
        }
    });
}

//Required for courses and course comments
function GetCourseByAcronym(acronym) {
    $.ajax({
        url: "/Course/GetByAcronym",  // Ruta al controlador y método
        type: "GET",
        data: { acronym: acronym },  // Parámetro acronym que se pasa al método
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result) {
                // Si la respuesta es exitosa, manejar los datos (ejemplo: mostrar información del curso)
                console.log(result);
                displayCourseInfo(result);  // Llamar a una función para manejar la información del curso

            } else {
                alert("No se encontró el curso.");
            }
        },
        error: function (errorMessage) {
            alert("Error al obtener el curso.");
        }
    });
}
//Required for courses and course comments
function displayCourseInfo(course) {
    $('#courseAcronym').text(course.acronym); // Se asume que 'course' tiene el atributo 'acronym'
    $('#courseName').text(course.name); // Se asume que 'course' tiene el atributo 'name'
    $('#courseDescription').text(course.description); // Se asume que 'course' tiene el atributo 'description'
    $('#assignedTeacher').text(course.professor.name); // Se asume que 'course' tiene el atributo 'teacher'
    GetCommentsByCourseId(course.acronym);

}


//Converters / Util stuff

function imageToBase64(imgElement, callback) {
    const img = imgElement; // Elemento <img>
    const canvas = document.createElement('canvas');
    const ctx = canvas.getContext('2d');

    canvas.width = img.width;
    canvas.height = img.height;
    ctx.drawImage(img, 0, 0);

    // Convertir la imagen a Base64 (formato PNG)
    const base64String = canvas.toDataURL('image/png');

    callback(base64String);
}


//Conversors
function imageToBase64(imgElement, callback) {
    const img = imgElement; // Elemento <img>
    const canvas = document.createElement('canvas');
    const ctx = canvas.getContext('2d');

    canvas.width = img.width;
    canvas.height = img.height;
    ctx.drawImage(img, 0, 0);

    // Convertir la imagen a Base64 (formato PNG)
    const base64String = canvas.toDataURL('image/png');

    callback(base64String);
}

//Required for courses and course comments
//Method for pass base64 to <img>
function base64ToImage(base64String, imgElement) {
    imgElement.src = `data:image/png;base64,${base64String}`;
}





//Required for courses and course comments
function openModal(modal) {
    modal.style.display = 'flex';
    navigationBar.style.display = 'none';
}
//Required for courses and course comments
function closeModal(modal) {
    modal.style.display = 'none';
    navigationBar.style.display = 'block';
}
function postComment() {
    var content = $("#textareacomment").val().trim(); // Obtiene el valor del textarea y quita espacios vacíos
    var acronym = $("#courseAcronym").text().trim(); // Obtiene el texto del h4

    const currentDate = new Date();
    const year = currentDate.getFullYear();
    const month = String(currentDate.getMonth() + 1).padStart(2, '0');
    const day = String(currentDate.getDate()).padStart(2, '0');
    const date = `${year}-${month}-${day}`;

    var commentData = {
        content: content,
        acronym: acronym,
        idUser: $("#studentID").text(), // Usar el ID del estudiante desde la sesión
        date: date
    };

    $.ajax({
        url: "/CommentCourse/Post",
        type: "POST",
        data: JSON.stringify(commentData),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#message").val("");
            GetCommentsByCourseId(acronym);
            $("#textareacomment").val("")
        },
        error: function (error) {
            alert("Error al enviar el comentario.");
            console.log(error);
        }
    });
}
function PutStudent() {
    
    let student = {
        id: $("#studentID").text(),
        name: $("#pName").val(),
        lastName: $("#pSname").val(),
        email: $("#pMail").val(),
        password: $("#pPassword").val(),
        likings: $("input[name='intereses']:checked").val()
    };



  
    $.ajax({
        url: "/Student/Put", // Endpoint del método en el controlador
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(student), // Convertir objeto a JSON
        dataType: "json",
        success: function (response) {
            GetStudentData(student.id);
            alert("Student updated successfully!");
        },
        error: function () {
            alert("Error updating student. Try again.");
        }
    });
}


closeButtons.forEach(button => {
    button.addEventListener('click', () => {
        const modal = button.closest('.custom-modal');
        closeModal(modal);
    });
});

//Required for courses and course comments
closeButtons.forEach(button => {
    button.addEventListener('click', () => {
        const modal = button.closest('.custom-modal-close');
        closeModal(modal);
    });
});

window.addEventListener('click', (event) => {
    modals.forEach(modal => {
        if (event.target === modal) {
            closeModal(modal);
        }
    });
});
const profileNav = document.getElementById('profileNav');
const profileModal = document.getElementById('profileModal');
profileNav.addEventListener('click', (event) => {
    event.preventDefault();
    openModal(profileModal);
});

const newsModal = document.getElementById('newsModal');
const moreAboutLinks = document.querySelectorAll(".more-about-link");
moreAboutLinks.forEach(link => {
    link.addEventListener('click', (event) => {
        event.preventDefault();
        openModal(newsModal);
    });
});

const mailNav = document.getElementById('emailNav');
const mailModal = document.getElementById('emailModal');

mailNav.addEventListener('click', (event) => {
    event.preventDefault();
    openModal(mailModal);
});
// Link Discussion button to Course Discussion modal
const discussionButton = document.getElementById('discussionButton');
const courseModal = document.getElementById('courseModal');
discussionButton.addEventListener('click', () => {
    openModal(courseModal);
});

function loadAllNewsDescending() {
    $.ajax({
        url: "/BreakingNew/GetMaxId",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (maxIdResult) {
            if (maxIdResult) {
                let maxId = maxIdResult.idNot;
                loadNewsById(maxId);
            } else {
                alert("No se pudo obtener el ID máximo.");
            }
        },
        error: function () {
            alert("Error al obtener el ID máximo.");
        }
    });
}

function loadNewsById(id) {
    if (id < 1) {
        updateNewsDisplay();
        return; // Detener si el ID es menor que 1
    }

    $.ajax({
        url: "/BreakingNew/Get",
        type: "GET",
        data: { idNot: id },
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (news) {
            if (news) {
                newsList.push(news);
            }
            // Intentar cargar la siguiente noticia
            loadNewsById(id - 1);
        },
        error: function () {
            // Manejar el caso en que el ID no exista y continuar con el siguiente
            loadNewsById(id - 1);
        }
    });
}

function updateNewsDisplay() {
    $("#newsContainer").empty();

    // Agregar los botones de navegación
    $("#newsContainer").append('<button class="carousel-control left" id="prevBtn"> &lt </button>');
    $("#newsContainer").append('<button class="carousel-control right" id="nextBtn">&gt</button>');

    for (let i = currentIndex; i < currentIndex + newsPerPage && i < newsList.length; i++) {
        addNewsComponent(newsList[i]);
    }

    // Mostrar u ocultar botones según el índice actual
    $("#prevBtn").toggle(currentIndex > 0);
    $("#nextBtn").toggle(currentIndex + newsPerPage < newsList.length);

    // Reasignar eventos de clic a los botones de navegación
    $("#prevBtn").click(function () {
        if (currentIndex > 0) {
            currentIndex -= newsPerPage;
            updateNewsDisplay();
        }
    });

    $("#nextBtn").click(function () {
        if (currentIndex + newsPerPage < newsList.length) {
            currentIndex += newsPerPage;
            updateNewsDisplay();
        }
    });
}

function addNewsComponent(news) {
    // Crear nuevo elemento HTML para la noticia
    var newsItem = `
        <div class="news-item">
            <img src="${news.imageUrl}" alt="News Image" class="news-image" />
            <div class="news-content">
                <h3 class="news-title">${news.title}</h3>
                <a href="#" class="more-about-link"
                   data-title="${news.title}"
                   data-date="${news.date}"
                   data-paragraph="${news.paragraph}"
                   data-photo="${news.photo ? btoa(String.fromCharCode.apply(null, news.photo)) : ''}">
                    <u>More about</u>
                </a>
            </div>
        </div>`;

    // Agregar la noticia al contenedor
    $("#newsContainer").append(newsItem);

    // Asignar evento de apertura del modal
    $("#newsContainer .more-about-link").last().on("click", function (e) {
        e.preventDefault(); // Evita la navegación por defecto

        // Obtener datos desde `data-attributes`
        var title = $(this).data("title");
        var date = $(this).data("date");
        var paragraph = $(this).data("paragraph");
        var photo = $(this).data("photo");

        // Rellenar el modal con la información correcta usando selectores de clase
        $("#newsModal .news-title").text(title);
        $("#newsModal .news-date").text("Published on: " + date);
        $("#newsModal .news-body").text(paragraph);

        // Manejar la imagen
        if (photo) {
            $("#newsModal .news-image").attr("src", "data:image/png;base64," + photo);
        } else {
            $("#newsModal .news-image").attr("src", "/images/default.jpg"); // Imagen por defecto
        }

        // Mostrar el modal
        $("#newsModal").fadeIn();
    });
}


