import {Component} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import SingInForm from "../../service/auth/contracts/SingInForm";
import {AuthService} from "../../service/auth/auth.service";
import {Router} from "@angular/router";


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  singInForm = new FormGroup({
      email: new FormControl("", [Validators.required]),
      password: new FormControl("", [Validators.required])
    }
  )

  constructor(private authService: AuthService, private router: Router) {
  }

  public onSubmit() {
    if (this.singInForm.valid) {
      this.authService
        .singIn(this.singInForm.value as SingInForm)
        .subscribe({
          next: x => {
            this.authService.setJwt(x.accessToken);
            this.router.navigate(['/']);
          },
          error: error => console.log(error)
        });
    }
  }
}
