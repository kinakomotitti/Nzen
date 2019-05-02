window.onload = Onload();

function Onload(){
    document.addEventListener('DOMContentLoaded', function () {
        document.getElementById('inputUserName').onchange = UserIdChangeEventHandler;
        document.getElementById('inputGroupId').onchange = GroupIdChangeEventHandler;
    }, false);
}

function UserIdChangeEventHandler(event){

    userName = document.getElementById('inputUserName').value;
    document.getElementById('inputUserName2').value = userName;
}

function GroupIdChangeEventHandler(event) {

    userName = document.getElementById('inputGroupId').value;
    document.getElementById('inputGroupId2').value = userName;
}
