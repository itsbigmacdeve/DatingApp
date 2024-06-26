import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/members';
import { map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members : Member[] = [];

  constructor(private http: HttpClient) { }

  //Este metodo obtiene todos los miembros
  getMembers() {
    if (this.members.length > 0) return of(this.members);
    return this.http.get<Member[]>(this.baseUrl + 'users').pipe(
      map(members => {this.members = members; return members;}));
  }


  //Este metodo obtiene un miembro por username
  getMember(username: string) {
    const member = this.members.find(x => x.userName === username);
    if (member) return of(member);
    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }

  updareMember(member: Member) {
    return this.http.put(this.baseUrl + 'users', member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = {...this.members[index], ...member};
      })
    );
  }

  setMainPhoto(photoId: number) {
    return this.http.put(this.baseUrl + 'users/set-main-photo/' + photoId, {});
  }

  deletePhoto(photoId: number) {
    return this.http.delete(this.baseUrl + 'users/delete-photo/' + photoId);
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
