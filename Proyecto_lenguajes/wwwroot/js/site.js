﻿const modals = document.querySelectorAll('.custom-modal');
const closeButtons = document.querySelectorAll('.custom-modal-close');
const navigationBar = document.getElementById('navBar');


let newsArray = [];
let newCurrentID = 1;

$(document).ready(function () {
   
    getStudentDataFromSession().then(student => {
        
        if (student) {
            GetCoursesByCycle(1);
            loadNews();
            LoadProfessor();

            //Required for courses and course comments
            const month = new Date().getMonth() + 1;

            const courseModal = document.getElementById('courseModal');

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


            $('#nextBtn').click(function () {
                moveNext();
                renderNews();
            });


            $('#prevBtn').click(function () {
                movePrev();
                renderNews();
            });

            GetStudentData();
            

            


        } else {
            swal.fire("Error", "No se encontraron datos del estudiante.", "error");
        }
    }).catch(() => {

        document.querySelector("#header").scrollIntoView({ behavior: "smooth" });//redirige al loggin
    });

});


function checkSession() {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: "/Student/IsSessionActive",
            type: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response && typeof response.isLoggedIn !== 'undefined') {
                   
                    resolve(response.isLoggedIn);
                } else {
                    reject(new Error("Respuesta inesperada del servidor"));
                }
            },
            error: function (xhr, status, error) {
                reject(new Error(`Error en la solicitud: ${status} - ${error}`));
            }
        });
    });
}
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
            swal.fire("Error", "Error abstrayendo la información de los profesores", "error");
        }
    });
}

function PostApplicationConsultation() {
    let appointmentType = $("#appointmentType").val(); // Obtener el valor del select

    if (appointmentType == "0") { // Solo ejecuta si el select está en "1"
        let professorSelect = $("#professorSelect option:selected"); // Obtiene la opción seleccionada
        let professorId = professorSelect.val(); // Obtiene el ID del profesor
        let professorName = professorSelect.text().replace(/\s*\(\d+\)$/, ''); // Extrae el nombre sin el ID

        getStudentDataFromSession().then(student => {
            if (student) {
                let applicationData = {
                    Text: $("#txtConsult").val(), // Asegurar que este input exista en el HTML
                    Student: {
                        Id: student.id, // Usar el ID del estudiante obtenido de la sesión
                        Name: student.name // Usar el nombre del estudiante obtenido de la sesión
                    },
                    Professor: {
                        Id: professorId,
                        Name: professorName
                    }
                };

                $.ajax({
                    url: "/ApplicationConsultation/Post",
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify(applicationData),
                    dataType: "json",
                    success: function (response) {
                        swal.fire("Success", "Consulta enviada exitosamente", "success");
                        $("#txtConsult").val('');
                    },
                    error: function (error) {
                        swal.fire("Error", "Error al enviar la consulta", "error");
                    }
                });
            } else {
                swal.fire("Error", "No se encontraron datos del estudiante.", "error");
            }
        }).catch(() => {
            swal.fire("Error", "Error al obtener los datos del estudiante.", "error");
        });
    } 
}

function PostPrivateConsultation() {
    let appointmentType = $("#appointmentType").val(); // Obtener el valor del select

    if (appointmentType == "1") { // Solo ejecuta si el select está en "0"
        let professorSelect = $("#professorSelect option:selected"); // Obtiene la opción seleccionada
        let professorId = professorSelect.val(); // Obtiene el ID del profesor
        let professorName = professorSelect.text().replace(/\s*\(\d+\)$/, ''); // Extrae el nombre sin el ID

        getStudentDataFromSession().then(student => {
            if (student) {
                let applicationData = {
                    Text: $("#txtConsult").val(), // Asegurar que este input exista en el HTML
                    Student: {
                        Id: student.id, // Usar el ID del estudiante obtenido de la sesión
                        Name: student.name // Usar el nombre del estudiante obtenido de la sesión
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
                        swal.fire("Success", "Consulta enviada exitosamente", "success");
                        $("#txtConsult").val('');
                    },
                    error: function (error) {
                        swal.fire("Error", "Error al enviar la consulta", "error");
                    }
                });
            } else {
                swal.fire("Error", "No se encontraron datos del estudiante.", "error");
            }
        }).catch(() => {
            swal.fire("Error", "Error al obtener los datos del estudiante.", "error");
        });
    } 
}

//Required for courses and course comments
function convertRomanToInt(roman) {
    const romanMap = { "I": 1, "II": 2, "III": 3, "IV": 4, "V": 5, "VI": 6, "VII": 7, "VIII": 8 };
    return romanMap[roman] || 0; 
}
function AuthenticateStudent() { 
    event.preventDefault();
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
            
            setTimeout(function () {
                location.reload();
            }, 0);
            setAsociation();
            // Vaciar los campos de entrada
            $("#lId").val('');
            $("#lPassword").val('');
        },
        error: function (xhr) {
            switch (xhr.status) {
                case 401:
                    swal.fire("Error", "Contraseña Incorrecta.", "error");
                    $("#lPassword").val('');
                    break;
                case 404:
                    swal.fire("Error", "El usuario consultado no existe.", "error");
                    $("#lId").val('');
                    $("#lPassword").val('');
                    break;
                case 403:
                    swal.fire("Error", "El usuario consultado no está activo.", "error");
                    $("#lId").val('');
                    $("#lPassword").val('');
                    break;
                default:
                    swal.fire("Error", "Error autentificando. Revisa las credenciales e intenta de nuevo.", "error");
                    $("#lId").val('');
                    $("#lPassword").val('');
                    break;
            }
        }
    });
}
function GetStudentData() {
    getStudentDataFromSession().then(student => {
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
            swal.fire("Error", "No se encontraron datos del estudiante.", "error");
        }
    }).catch(() => {

        document.querySelector("#header").scrollIntoView({ behavior: "smooth" });//redirige al loggin
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
            swal.fire("Success", "¡Estudiante registrado exitosamente!", "success");

            $("#rId").val('');
            $("#rName").val('');
            $("#rSname").val('');
            $("#rMail").val('');
            $("#rPassword").val('');
        },
        error: function () {
            swal.fire("Error", "Error en el registro del estudiante. Intenta de nuevo", "error");
        }
    });
}
//Required for courses and course comments
function loadNewsComments(id) {
    getStudentDataFromSession().then(student => {
        if (student) {
            var image = document.getElementById("imgNewComment");
            base64ToImage(student.photo, image)
        } else {
            swal.fire("Error", "No se encontraron datos del estudiante.", "error");
        }
    }).catch(() => {

        document.querySelector("#header").scrollIntoView({ behavior: "smooth" });//redirige al loggin
    });

    $("#newCommentmessage").val("");

    $.ajax({
        url: "/CommentNew/GetAll/",
        type: "GET",
        data: { id: id },
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $("#NewsCommentsLoader").empty();

            if (result.length > 0) {
                let promises = result.map(comment => {
                    return new Promise((resolve, reject) => {
                        let photo = "/images/defaultpfp.jpg";
                        let name = "Unknown!";

                        CheckNewsCommentType(comment.idUser)
                            .then(type => {
                                if (type == 1) {
                                    return GetProfessorCommentData(comment.idUser);
                                } else if (type == -1) {
                                    return GetStudentCommentData(comment.idUser);
                                }
                                return null;
                            })
                            .then(userData => {
                                if (userData) {
                                    photo = userData.photo || photo;
                                    name = userData.name || name;
                                }
                                resolve({ name, photo, comment });
                            })
                            .catch(error => {
                                console.error("Error:", error);
                                resolve({ name, photo, comment }); // Evita que un error detenga todo
                            });
                    });
                });

                // Esperamos a que todas las promesas se resuelvan antes de mostrar los comentarios
                Promise.all(promises).then(commentsData => {
                    commentsData.forEach(({ name, photo, comment }) => {
                        appendComment(name, photo, comment);
                    });
                });
            }
        },
        error: function (errorMessage) {

            
            console.error("Error al cargar los comentarios", errorMessage);


            
        }
    });
}
function appendComment(name, photo, comment) {
    let photoSrc = photo.startsWith("data:image") ? photo : `data:image/jpeg;base64,${photo}`;
    var commentHtml = `
            <div class="media">
                <div class="col-sm-3 col-lg-2 hidden-xs">
                    <img class="comment-media-object" src="${photoSrc}" alt="User Profile">
                </div>
                <div class="comment col-xs-12 col-sm-9 col-lg-10">
                    <h4 class="media-heading">${name}</h4>
                    <h5 class="media-heading">${comment.date}</h5>
                    <p>${comment.content}</p>
                </div>
            </div>
        `;
    $("#NewsCommentsLoader").append(commentHtml);
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

    getStudentDataFromSession().then(student => {
        if (student) {
            var image = document.getElementById("imageUser");
            if (student.photo) {
                base64ToImage(student.photo, image)
            } else {
                $('#imageUser').attr("src", "/images/default.jpg")
            }
            
        } else {
            swal.fire("Error", "No se encontraron datos del estudiante.", "error");
        }
    }).catch(() => {
        
        document.querySelector("#header").scrollIntoView({ behavior: "smooth" });//redirige al loggin
    });

   

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
                swal.alert("Error", "Error al cargar la foto", "error");
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
function GetStudentPhotoById(imgId) {
    $.ajax({
        url: "/Student/GetStudentDataFromSession",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (student) {
            if (student) {
                const imgElement = document.getElementById(imgId);
                if (imgElement) {
                    base64ToImage(student.photo, imgElement);
                } else {
                    console.error(`Elemento con ID ${imgId} no encontrado.`);
                }
            } else {
                swal.fire("Error", "No se encontraron datos del estudiante.", "error");
            }
        },
        error: function () {

            swal.file("Error", "Error al obtener los datos del estudiante.", "error");

            document.querySelector("#header").scrollIntoView({ behavior: "smooth" });//redirige al loggin
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
                swal.fire("Error", "No se encontró el curso.", "error");
            }
        },
        error: function (errorMessage) {
            swal.fire("Error", "Error al obtener el curso.", "error");
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
function postNewsComment() {
    var contentC = $("#newCommentmessage").val().trim(); // Usar .val() para obtener el valor del textarea
    var idNew = newsArray[newCurrentID].idNew;

    const currentDate = new Date();
    const year = currentDate.getFullYear();
    const month = String(currentDate.getMonth() + 1).padStart(2, '0');
    const day = String(currentDate.getDate()).padStart(2, '0');
    const date = `${year}-${month}-${day}`;

    getStudentDataFromSession().then(student => {
        if (student) {
            var commentData = {
                content: contentC,
                idNew: idNew,
                date: date,
                idUser: student.id
            };

            postNewCommentData(commentData);
        } else {
            swal.fire("Error", "No se encontraron datos del estudiante.", "error");
        }
    }).catch(() => {
        swal.fire("Error", "Error al obtener los datos del estudiante.", "error");

        document.querySelector("#header").scrollIntoView({ behavior: "smooth" });//redirige al loggin
    });
}

function setAsociation() {
    getStudentDataFromSession()
        .then(student => {
            if (student && student.asociation == "1") {
                $("#addNewsButton").removeAttr("hidden");
            } else {
                $("#addNewsButton").attr("hidden", "true");
            }
        })
        .catch(error => {
            console.error("Error obteniendo datos del estudiante:", error);
        });
}

function postNewCommentData(commentData) {
    $.ajax({
        url: "/CommentNew/Post",
        type: "POST",
        data: JSON.stringify(commentData),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#newsCommentText").val("");
            loadNewsComments(commentData.idNew);
        },
        error: function (error) {
            swal.fire("Error", "Error al enviar el comentario.", "error");
        }
    });
}
function getStudentDataFromSession() {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: "/Student/GetStudentDataFromSession",
            type: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (student) {
                
                resolve(student);

            },
            error: function () {
                reject();
                document.querySelector("#header").scrollIntoView({ behavior: "smooth" });//redirige al loggin
            }
        });
    });
}

//Required for courses and course comments
function openModal(modal) {
    modal.style.display = 'flex';
    navigationBar.style.display = 'none';
    document.getElementById('nextBtn').style.display = "none";
    document.getElementById('prevBtn').style.display = "none";

}
//Required for courses and course comments
function closeModal(modal) {
    modal.style.display = 'none';
    navigationBar.style.display = 'flex';
    document.getElementById('nextBtn').style.display = "block";
    document.getElementById('prevBtn').style.display = "block";
}
function postComment() {

    var content = $("#textareacomment").val().trim(); // Obtiene el valor del textarea y quita espacios vacíos
    var acronym = $("#courseAcronym").text().trim(); // Obtiene el texto del h4

    const currentDate = new Date();
    const year = currentDate.getFullYear();
    const month = String(currentDate.getMonth() + 1).padStart(2, '0');
    const day = String(currentDate.getDate()).padStart(2, '0');
    const date = `${year}-${month}-${day}`;


    getStudentDataFromSession().then(student => {
        if (student) {
            var commentData = {
                content: content,
                acronym: acronym,
                idUser: student.id, // Usar el ID del estudiante desde la sesión
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
                    $("#textareacomment").val("");
                },
                error: function (error) {
                    swal.fire("Error", "Error al enviar el comentario.", "error");
                    console.log(error);
                }
            });
        } else {
            swal.fire("Error", "No se encontraron datos del estudiante.", "error");
        }
    }).catch(() => {
        swal.fire("Error", "Error al obtener los datos del estudiante.", "error");

        document.querySelector("#header").scrollIntoView({ behavior: "smooth" });//redirige al loggin
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
            swal.fire("Success", "¡Estudiante actualizado exitosamente!", "success");
        },
        error: function () {
            swal.fire("Error", "Error al actualizar el estudiante. Intenta de nuevo", "error");
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
    checkSession().then(isLoggedIn => {
        if (isLoggedIn) {
            GetStudentPhotoById('imageUser');
        } else {
           
        }
    }).catch(error => {
        console.error("Error al verificar la sesión:", error);
    });
    openModal(courseModal);
});

const asoModal = document.getElementById('asoModal');
const addNewsButton = document.getElementById('addNewsButton');

addNewsButton.addEventListener('click', (event) => {
    event.preventDefault();
    openModal(asoModal);
});



function loadNews() {
    $.ajax({
        url: "/BreakingNew/Get",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result, function (key, item) {
                newsArray.push({
                    idNew: item.idNew,
                    title: item.title,
                    paragraph: item.paragraph,
                    photo: item.photo,
                    date: item.date
                });
            });
            renderNews();
        },
        error: function (errorMessage) {
            alert("Error fetching news");
        }
    });
}

let newsBase64 = "";
let newsPure64 = "";

document.getElementById("fileInputNew").addEventListener("change", function (event) {
    const file = event.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
            newsBase64 = e.target.result;
            newsPure64 = newsBase64.split(",")[1];
            const preview = document.getElementById("imageAddPreview");
            preview.style.backgroundImage = `url(${e.target.result})`;
            preview.style.backgroundSize = "cover";
            preview.style.backgroundPosition = "center";
            preview.style.width = "200px";  
            preview.style.height = "200px";
        };
        reader.readAsDataURL(file);
    }
});

function postNew() {
    event.preventDefault();

    var title = document.getElementById('newsTitle').value;
    var paragraph = document.getElementById('newsParagraph').value;
    var photo = newsPure64;

    console.log("auxilio");

    let news = {
        Title: title,
        Paragraph: paragraph,
        Photo: photo
    };

    if (title == "" || paragraph == "" || photo == "") {


        alert("Falta información para la noticia");


    } else {

        $.ajax({
            url: "/BreakingNew/Post",
            type: "POST",
            data: JSON.stringify(news),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (response) {
                loadNews();
                clearNewsForm();
            },
            error: function (error) {
                swal.fire("Error", "Error al publicar la noticia.", "error");
            }
        });


    }
}

function clearNewsForm(){

    document.getElementById('newsTitle').value = "";
    document.getElementById('newsParagraph').value = "";
    newsBase64 = "";
    newsPure64 = "";
    document.getElementById('imageAddPreview').style.backgroundImage = "";
    closeModal(asoModal);


}





function renderNews() {
    document.getElementById('newImage0').src = `data:image/png;base64,${newsArray[0].photo}`;
    document.getElementById('newImage1').src = `data:image/png;base64,${newsArray[1].photo}`;
    document.getElementById('newImage2').src = `data:image/png;base64,${newsArray[2].photo}`;

    document.getElementById('newPreviewTittle0').textContent = newsArray[0].title;
    document.getElementById('newPreviewTittle1').textContent = newsArray[1].title;
    document.getElementById('newPreviewTittle2').textContent = newsArray[2].title;
}

function showNew() {
    document.getElementById('newFullImage').src = `data:image/png;base64,${newsArray[newCurrentID].photo}`;
    document.getElementById('newTitle').textContent = newsArray[newCurrentID].title;
    document.getElementById('newDate').textContent = newsArray[newCurrentID].date;
    document.getElementById('newBody').textContent = newsArray[newCurrentID].paragraph;
    loadNewsComments(newsArray[newCurrentID].idNew);


    checkSession().then(isLoggedIn => {
        if (isLoggedIn) {
            GetStudentPhotoById('imgNewComment');


        } else {
            
        }
    }).catch(error => {
        console.error("Error al verificar la sesión:", error);
    });
    

}

function GetProfessorCommentData(id) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: "/CommentNew/GetProfessorCommentData/",
            type: "GET",
            data: { id: id },
            dataType: "json",
            success: function (result) {
                resolve(result);
            },
            error: function () {

                reject("Error retrieving data");
            }
        });
    });
}

function CheckNewsCommentType(id) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: "/CommentNew/CheckType/",
            type: "GET",
            data: { id: id },
            contentType: "application/json;charset=utf-8",
            dataType: "text",
            success: function (result) {
                resolve(result);
            },
            error: function () {
                swal.fire("Error", "Error al verificar el tipo de usuario.", "error");
                reject("Error en la petición");
            }
        });
    });
}


function GetStudentCommentData(id) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: "/CommentNew/GetStudentCommentData/",
            type: "GET",
            data: { id: id },
            dataType: "json",
            success: function (result) {
                resolve(result);
            },
            error: function () {
                swal.fire("Error", "Error al obtener los datos del estudiante.", "error");
                reject("Error retrieving data");
            }
        });
    });
}

function GetApplicationConsultationByStudent() {
    getStudentDataFromSession().then(student => {
        if (student) {
            $.ajax({
                url: "ApplicationConsultation/GetByStudent/",
                type: "GET",
                data: { id: student.id },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    $('#previous_text').attr('hidden', false);
                    $('#student_app_photo').attr('hidden', true);
                    $('#student_app_name').attr('hidden', true);
                    $('#student_app_date').attr('hidden', true);
                    $('#student_app_text').attr('hidden', true);
                    $('#student_app_reply').attr('hidden', true);
                    $('#student_app_answer').attr('hidden', true);
                    $('#sender_app').attr('hidden', true);

                    var htmlTable = '';

                    $.each(result, function (key, item){
                        htmlTable += '<tr>';
                        htmlTable += '<td class="email-info">';
                        htmlTable += '<h3> RE:' + item.text + '</h3>';
                        htmlTable += '<p>De: ' + item.professor.name + ' ' + item.professor.lastName + '</p>';

                        htmlTable += '</td>';
                        htmlTable += '<td class="email-action">';
                        htmlTable += '<button class="btn btn-primary" onclick="LoadSpecificAppConsultation(' + item.id + ')">Abrir</button>';
                        htmlTable += '</td>';
                        htmlTable += '</tr>';

                    }) //end foreach

                    $('#email-table-appointment').empty();
                    $('#email-table-appointment').html(htmlTable);


                }, error: function () {
                    swal.fire("Error", "Error al obtener las consultas.", "error");
                    reject("Error retrieving data");
                }
            })//endajax
        }//endif
    })
}

function LoadSpecificAppConsultation(id) {
    $.ajax({
        url: "ApplicationConsultation/GetById/",
        type: "GET",
        data: { id: id },
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#previous_text').attr('hidden', true);
            $('#student_app_photo').attr('hidden', false);
            $('#student_app_name').attr('hidden', false);
            $('#student_app_date').attr('hidden', false);
            $('#student_app_text').attr('hidden', false);
            $('#student_app_reply').attr('hidden', false);
            $('#student_app_answer').attr('hidden', false);
            $('#sender_app').attr('hidden', true);

            if (result.professor.photo) {
                $('#student_app_photo').attr("src", `data:image/png;base64,${result.professor.photo}`)
            } else {
                $('#student_app_photo').attr("src", "/images/default.jpg")
            };

            $('#student_app_name').text(result.professor.name + " " + result.professor.lastName);
            $('#student_app_date').text(result.date);
            $('#student_app_text').text(result.text);
            $('#student_app_answer').text(result.answer);

            $("#student_app_answer").prop("readonly", true);

            $('#student_app_idconsult').text(id.toString());
            $('#student_app_student').text(result.idStudent);
            $('#student_app_professor').text(result.idProfessor);
        },//end success
        error: function () {
            swal.fire("Error", "Error al obtener los datos de la consulta.", "error");
            reject("Error retrieving data");
        } //end error
    })//end ajax
} //end function

function GetPrivateConsultationByStudent() {
    getStudentDataFromSession().then(student => {
        if (student) {
            $.ajax({
                url: "PrivateConsultation/GetByStudent/",
                type: "GET",
                data: { id: student.id },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    $('#previous_priv_text').attr('hidden', false);
                    $('#student_private_photo').attr('hidden', true);
                    $('#student_private_name').attr('hidden', true);
                    $('#student_private_date').attr('hidden', true);
                    $('#student_private_text').attr('hidden', true);
                    $('#student_private_reply').attr('hidden', true);
                    $('#student_private_answer').attr('hidden', true);
                    $('#sender_private').attr('hidden', true);

                    var htmlTable = '';

                    $.each(result, function (key, item) {
                        htmlTable += '<tr>';
                        htmlTable += '<td class="email-info">';
                        htmlTable += '<h3> RE: ' + item.text + '</h3>';
                        htmlTable += '<p>De: ' + item.professor.name + ' ' + item.professor.lastName + '</p>';

                        htmlTable += '</td>';
                        htmlTable += '<td class="email-action">';
                        htmlTable += '<button class="btn btn-primary" onclick="LoadSpecificPrivateConsultation(' + item.id + ')">Abrir</button>';
                        htmlTable += '</td>';
                        htmlTable += '</tr>';

                    }) //end foreach

                    $('#email-table-private').empty();
                    $('#email-table-private').html(htmlTable);


                }, error: function () {
                    swal.fire("Error", "Error al obtener las consultas.", "error");
                    reject("Error retrieving data");
                }
            })//endajax
        }//endif
    })
}

function LoadSpecificPrivateConsultation(id) {
    $.ajax({
        url: "PrivateConsultation/GetById/",
        type: "GET",
        data: { id: id },
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#previous_priv_text').attr('hidden', true);
            $('#student_private_photo').attr('hidden', false);
            $('#student_private_name').attr('hidden', false);
            $('#student_private_date').attr('hidden', false);
            $('#student_private_text').attr('hidden', false);
            $('#student_private_reply').attr('hidden', false);
            $('#student_private_answer').attr('hidden', false);
            $('#sender_private').attr('hidden', true);

            if (result.professor.photo) {
                $('#student_private_photo').attr("src", `data:image/png;base64,${result.professor.photo}`)
            } else {
                $('#student_private_photo').attr("src", "/images/default.jpg")
            };

            $('#student_private_name').text(result.professor.name + " " + result.professor.lastName);
            $('#student_private_date').text(result.date);
            $('#student_private_text').text(result.text);
            $('#student_private_answer').text(result.answer);

            $("#student_private_answer").prop("readonly", true);

            $('#student_private_idconsult').text(id.toString());
            $('#student_private_student').text(result.idStudent);
            $('#student_private_professor').text(result.idProfessor);
        },//end success
        error: function () {
            swal.fire("Error", "Error al obtener los datos de la consulta.", "error");
            reject("Error retrieving data");
        } //end error
    })//end ajax
}


function moveNext() {
    let firstItem = newsArray.shift();
    newsArray.push(firstItem);
}


function movePrev() {

    let lastItem = newsArray.pop();
    newsArray.unshift(lastItem);

}

const link0 = document.getElementById("newsLink0");

const link1 = document.getElementById("newsLink1");

const link2 = document.getElementById("newsLink2");

link0.addEventListener('click', (event) => {
    newCurrentID = 0;
    showNew();
});

link1.addEventListener('click', (event) => {
    newCurrentID = 1;
    showNew();
});

link2.addEventListener('click', (event) => {
    newCurrentID = 2;
    showNew();

})

link0.addEventListener('click', (event) => {
    event.preventDefault();
    openModal(newsModal);

});

link1.addEventListener('click', (event) => {
    event.preventDefault();
    openModal(newsModal);

});

link2.addEventListener('click', (event) => {
    event.preventDefault();
    openModal(newsModal);

});
