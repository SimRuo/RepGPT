// This is not safe at all and this should never be done in a proper production.
// as such please dont enter any information into this app that you do not want everyone to be able to access.

export function loginAs(userId) {
    localStorage.setItem('userId', userId);
}

export function getLoggedInUserId() {
    return parseInt(localStorage.getItem('userId'), 10);
}

