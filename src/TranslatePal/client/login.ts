import {inject, Aurelia} from 'aurelia-framework';

// TODO: Call our API server for authentication.

@inject(Aurelia)
export class Login {

    constructor(aurelia: Aurelia) {

        this.aurelia = aurelia;
    }

    public username = '';
    public password = '';

    attached() {

        let isLoggedIn: string = localStorage.getItem('loggedIn');
        if (new Boolean(isLoggedIn).valueOf()) {

            this.aurelia.setRoot('app');
        }
    }

    public login(evt: Event) {

        if (this.username === 'user' && this.password === 'test') {

            localStorage.setItem('loggedIn', true.toString());
            this.aurelia.setRoot('app');
        }

        evt.preventDefault();
    }

    private aurelia: Aurelia = null;
}