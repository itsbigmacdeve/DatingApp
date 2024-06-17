import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { Form, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  //@Input() usersFromHomeComponent: any;
  @Output() cancelRegister = new EventEmitter(); // Se crea un evento para cancelar el registro
  model: any = {};
  registerForm: FormGroup = new FormGroup({}); 

  constructor(
    private accountService: AccountService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {//TODO ESTE METODO CAMBIO
    this.registerForm = new FormGroup({
      username: new FormControl("Hello", Validators.required ),
      password: new FormControl("" , [Validators.required, Validators.minLength(4), Validators.maxLength(8)]),
      confirmPassword: new FormControl(),
    });
  }

  register() {
    console.log(this.registerForm?.value); //AQUI ESTOY CAMBIO
    // this.accountService.register(this.model).subscribe({
    //   next: () => {
    //     this.cancel();
    //   },
    //   error: (error) => {
    //     this.toastr.error(error.error);
    //     console.log(error);
    //   },
    // });
  }

  cancel() {
    this.cancelRegister.emit(false); // Despues de crea el metodo para cancelar la emsiion del evento
  }
}
