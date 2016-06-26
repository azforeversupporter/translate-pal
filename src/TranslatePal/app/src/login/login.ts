import {inject, Aurelia} from 'aurelia-framework';
import {AuthService} from 'aurelia-auth';

@inject(Aurelia, AuthService)
export class Login {

    constructor(aurelia: Aurelia, authService: AuthService) {

        this.aurelia = aurelia;
        this.authService = authService;
    }

    public username = null;
    public password = null;

    public login(evt: Event): void {

        this.authService
            .login(`grant_type=password&username=${this.username}&password=${this.password}&scope=email profile roles`, undefined)
            .then(response => {

                console.log('Success logged: ' + response);
                this.clearCredentials();
                this.aurelia.setRoot('dashboard/dashboard');
            })
            .catch(err => {

                this.clearCredentials();
                console.error('Login failure');
            });

        evt.preventDefault();
    }

    private clearCredentials(): void {

        this.username = null;
        this.password = null;
    }

    private aurelia: Aurelia;
    private authService: AuthService;
}