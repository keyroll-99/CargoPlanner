import {ComponentFixture, TestBed} from '@angular/core/testing';

import {SignInComponent} from './sign-in.component';
import {Router} from "@angular/router";
import {lastValueFrom, of} from "rxjs";
import {AuthService} from "../../../../core/services/auth.service";
import {MatInputModule} from "@angular/material/input";
import {MatCardModule} from "@angular/material/card";
import {MatButtonModule} from "@angular/material/button";
import {NgIconsModule} from "@ng-icons/core";
import {bootstrapArrowRightShort} from "@ng-icons/bootstrap-icons";
import {MatListModule} from "@angular/material/list";
import {ReactiveFormsModule} from "@angular/forms";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import AuthModel from "../../../../core/models/authModel";

describe('SignInComponent', () => {
  let component: SignInComponent;
  let fixture: ComponentFixture<SignInComponent>;

  const mockRouter = jasmine.createSpyObj<Router>(['navigate']);
  mockRouter.navigate.and.returnValue(lastValueFrom(of(true)))

  const mockAuthService = jasmine.createSpyObj<AuthService>(['singIn', "setAuth"])


  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SignInComponent],
      providers: [
        {provide: AuthService, useValue: mockAuthService},
        {provide: Router, useValue: mockRouter}
      ],
      imports:[
        MatInputModule,
        MatCardModule,
        MatButtonModule,
        NgIconsModule.withIcons({bootstrapArrowRightShort}),
        MatListModule,
        ReactiveFormsModule,
        BrowserAnimationsModule
      ]
    });
    fixture = TestBed.createComponent(SignInComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call service when form is valid', () => {
    // Arrange
    component.singInForm.setValue({
      email: "myMail@gmail.com",
      password: "superStrongPassword"
    })

    const response : AuthModel = {
      email: "email",
      userId: "user",
      expires: new Date(),
      accessToken: "token"
    }

    mockAuthService.singIn.and.returnValue(of(response))


    // Act
    component.onSubmit();

    // Assert
    expect(mockAuthService.singIn).toHaveBeenCalled();
    expect(mockRouter.navigate).toHaveBeenCalled()
  })
});
