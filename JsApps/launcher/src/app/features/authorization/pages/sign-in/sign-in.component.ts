import {Component} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import SingInForm from "../../models/SingInForm";
import {AuthService} from "../../../../core/services/auth.service";

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent {
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
          error: (error: string) => console.log(error)
        });
    }
  }
}
