import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { MembersService } from '../_services/members.service';
import { Member } from '../_models/members';

export const membersDetailedResolver: ResolveFn<Member> = (route, state) => {
  const memberService = inject(MembersService);

  return memberService.getMember(route.paramMap.get('username')!);
};
