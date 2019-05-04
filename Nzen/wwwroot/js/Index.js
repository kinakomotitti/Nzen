window.onload = Onload();

function Onload() {
    document.addEventListener('DOMContentLoaded', function () {
        document.getElementById('inputUserName').onchange = UserIdChangeEventHandler;
        document.getElementById('inputGroupId').onchange = GroupIdChangeEventHandler;
        document.getElementById('joinAudienceButton').onclick = JoinAsAudienceButtonClick;
        document.getElementById('joinPresenterButton').onclick = JoinAsPresenterButtonClick;
    }, false);
}

function UserIdChangeEventHandler(event) {

    userName = document.getElementById('inputUserName').value;
    document.getElementById('inputUserName2').value = userName;
}

function GroupIdChangeEventHandler(event) {

    userName = document.getElementById('inputGroupId').value;
    document.getElementById('inputGroupId2').value = userName;
}

function JoinAsAudienceButtonClick(event) {

    if (document.getElementById('inputGroupId').value === '' ||
        document.getElementById('inputGroupId').value == null ||
        document.getElementById('inputUserName').value === '' ||
        document.getElementById('inputUserName').value == null) {

        document.getElementById('errorLavel').innerText = 'ユーザーIDとグループIDを入力してください。';
        document.getElementById('messageBox').hidden = false;
        return false;
    }
}

function JoinAsPresenterButtonClick(event) {

    if (document.getElementById('inputUserName').value === '') {

        document.getElementById('errorLavel').innerText = 'ユーザーIDを入力してください。';
        document.getElementById('messageBox').hidden = false;
        return false;
    }
}

