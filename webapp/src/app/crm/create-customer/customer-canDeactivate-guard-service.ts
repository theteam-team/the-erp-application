import { CanDeactivate } from '@angular/router';
import { CreateCustomerComponent } from './create-customer.component';
 

export class CustomerCanDeactivateGuardService implements CanDeactivate<CreateCustomerComponent> {
  canDeactivate(component: CreateCustomerComponent): boolean {
    if (component.createCustomerForm.dirty) {
      return confirm('Are you sure you want to discard your changes?');
    }
    return true;
  }
}
