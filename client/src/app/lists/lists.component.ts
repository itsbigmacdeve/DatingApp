import { Component, OnInit } from '@angular/core';
import { Member } from '../_models/members';
import { MembersService } from '../_services/members.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css'],
})
export class ListsComponent implements OnInit {
  members: Member[] | undefined;
  predicate = 'liked';

  constructor(private memberService: MembersService) {}

  ngOnInit(): void {
    this.loadLikes();
  }

  loadLikes() {
    this.memberService.getLikes(this.predicate).subscribe({
      next: (response) => {
        console.log(this.predicate);
        this.members = response;
      },
    });
  }
}
