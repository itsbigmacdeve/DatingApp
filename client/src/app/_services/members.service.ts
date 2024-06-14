import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/members';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  //Este metodo obtiene todos los miembros
  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + 'userscontrollers');
  }

  //Este metodo obtiene un miembro por username
  getMember(username: string) {
    return this.http.get<Member>(this.baseUrl + 'userscontrollers/' + username);
  }


  //Este metodo obtiene un miembro por id, pero le pasa de header el token, YA NO SE OCUPA, AHORA ES CON EL INTERCEPTOR
  // getHttpOptions() {

  //   const userString = localStorage.getItem('user');
  //   if (!userString) return ;
  //   const user = JSON.parse(userString);
  //   return {
  //     headers: {
  //       Authorization: 'Bearer ' + user.token
  //     }
  //   };
  // }
    
    
}
