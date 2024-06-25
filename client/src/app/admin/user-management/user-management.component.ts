import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { User } from 'src/app/_models/user';
import { AdminService } from 'src/app/_services/admin.service';
import { RolesModalComponent } from 'src/app/modals/roles-modal/roles-modal.component';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {
  users: any; // no se por que no me deja ponerlo de tipo USER
  bsModalRef: BsModalRef<RolesModalComponent> = new BsModalRef<RolesModalComponent>();


  constructor(private adminService: AdminService, private modalService: BsModalService ) { }

  ngOnInit(): void {
    this.getUserWithRoles();
  }

  getUserWithRoles() {
    this.adminService.getUsersWithRoles().subscribe({
      next: users => this.users = users
    })
  }

  openModal(){
    this.bsModalRef = this.modalService.show(RolesModalComponent);
  }

  openRolesModal(user: User){
    const initialState = {
      list: [
        'Do thing',
        'Do another thing',
        'Do something else'
      ]
    };
  }

}
