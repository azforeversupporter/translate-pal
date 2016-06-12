import {inject, Aurelia} from 'aurelia-framework';
import {AuthService} from 'aurelia-auth';

@inject(Aurelia, AuthService)
export class Login {

    constructor(aurelia: Aurelia, authService: AuthService) {

        this.aurelia = aurelia;
        this.authService = authService;
    }

    public username = '';
    public password = '';
    public rememberUsername = true;

    public attached() {

        setTimeout(() => (<any>$).material.init(), 100);
    }

    public login(evt: Event) {

        this.authService
            .login(`grant_type=password&username=${this.username}&password=${this.password}&scope=email profile roles`, undefined)
            .then(response => {
                console.log('success logged: ' + response);
                this.clearCredentials();
                this.aurelia.setRoot('app');
            })
            .catch(err => {
                
                this.clearCredentials();
                console.log('login failure');
            });

        evt.preventDefault();
    }

    private clearCredentials(): void {
        this.username = null;
        this.password = null;
    }

    private aurelia: Aurelia = null;
    private authService: AuthService;
}