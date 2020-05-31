const uri = 'api/IngrTypes';
let ingrTypes = [];

function getIngrTypes() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayIngrTypes(data))
        .catch(error => console.error('Unable to get categories.', error));
}

function addIngrType() {
    const addNameTextbox = document.getElementById('add-name');
    //const addInfoTextbox = document.getElementById('add-info');

    const ingrType = {
        name: addNameTextbox.value.trim(),
        //info: addInfoTextbox.value.trim(),
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(ingrType)
    })
        .then(response => response.json())
        .then(() => {
            getIngrTypes();
            addNameTextbox.value = '';
            //addInfoTextbox.value = '';
        })
        .catch(error => console.error('Unable to add category.', error));
}

function deleteIngrType(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getIngrTypes())
        .catch(error => console.error('Unable to delete category.', error));
}

function displayEditForm(id) {
    const ingrType = ingrTypes.find(ingrType => ingrType.id === id);

    document.getElementById('edit-id').value = ingrType.id;
    document.getElementById('edit-name').value = ingrType.name;
    //document.getElementById('edit-info').value = category.info;
    document.getElementById('editForm').style.display = 'block';
}

function updateIngrType() {
    const ingrTypeId = document.getElementById('edit-id').value;
    const ingrType = {
        id: parseInt(ingrTypeId, 10),
        name: document.getElementById('edit-name').value.trim(),
        //info: document.getElementById('edit-info').value.trim()
    };

    fetch(`${uri}/${ingrTypeId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(ingrType)
    })
        .then(() => getIngrTypes())
        .catch(error => console.error('Unable to update category.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}


function _displayIngrTypes(data) {
    const tBody = document.getElementById('ingrTypes');
    tBody.innerHTML = '';


    const button = document.createElement('button');

    data.forEach(ingrType => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${ingrType.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteIngrType(${ingrType.id})`);

        let tr = tBody.insertRow();


        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(ingrType.name);
        td1.appendChild(textNode);

        //let td2 = tr.insertCell(1);
        //let textNodeInfo = document.createTextNode(category.info);
        //td2.appendChild(textNodeInfo);

        let td2 = tr.insertCell(1);
        td2.appendChild(editButton);

        let td3 = tr.insertCell(2);
        td3.appendChild(deleteButton);
    });

    ingrTypes = data;
}
