import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/members';
import { map, of } from 'rxjs';
import { PaginatedResult } from '../_models/pagination';
import { UserParams } from '../_models/userParams';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members : Member[] = [];
  

  constructor(private http: HttpClient) { }

  //Este metodo obtiene todos los miembros

  getMembers(userParams : UserParams) {
    let params = this.getPaginationHeaders(userParams.pageNumber, userParams.pageSize);

    params = params.append('minAge', userParams.minAge);
    params = params.append('maxAge', userParams.maxAge);
    params = params.append('gender', userParams.gender);
    params = params.append('orderBy', userParams.orderBy);

    return this.getPaginatedResult<Member[]>( this.baseUrl + 'userscontrollers' ,params);
  }



  private getPaginatedResult<T>(url: string, params: HttpParams) {
    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>;
    return this.http.get<T>(url, { observe: 'response', params }).pipe(
      map(response => {
        if (response.body) {
          paginatedResult.result = response.body;
        }
        const pagination = response.headers.get('Pagination');
        if (pagination) {
          paginatedResult.pagination = JSON.parse(pagination);
        }
        return paginatedResult;
      })
    );
  }

  private getPaginationHeaders(pagNumber: number, pageSize: number) {
    let params = new HttpParams();

    
    params = params.append('pageNumber', pagNumber);
    params = params.append('pageSize', pageSize);
    
    return params;
  }

  //Este metodo obtiene un miembro por username
  getMember(username: string) {
    const member = this.members.find(x => x.userName === username);
    if (member) return of(member);
    return this.http.get<Member>(this.baseUrl + 'userscontrollers/' + username);
  }

  updareMember(member: Member) {
    return this.http.put(this.baseUrl + 'userscontrollers', member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = {...this.members[index], ...member};
      })
    );
  }

  setMainPhoto(photoId: number) {
    return this.http.put(this.baseUrl + 'userscontrollers/set-main-photo/' + photoId, {});
  }

  deletePhoto(photoId: number) {
    return this.http.delete(this.baseUrl + 'userscontrollers/delete-photo/' + photoId);
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
