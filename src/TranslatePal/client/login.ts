import {inject, Aurelia} from 'aurelia-framework';
import {AuthService} from './common/services/auth-service';

@inject(Aurelia, AuthService)
export class Login {

    constructor(aurelia: Aurelia, authService: AuthService) {

        this.aurelia = aurelia;
        this.authService = authService;
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
        
        this.authService
            .login(this.username, this.password)
            .then((success) => {

                if (success) {

                    this.aurelia.setRoot('app');
                }
                else {

                    alert('No valid credentials');
                }

                this.username = null;
                this.password = null;
            });

        evt.preventDefault();
    }

    private aurelia: Aurelia = null;
    private authService: AuthService;
}