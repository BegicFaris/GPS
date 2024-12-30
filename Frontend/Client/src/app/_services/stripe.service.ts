import { Injectable } from '@angular/core';
import { loadStripe, Stripe, StripeCardElement } from '@stripe/stripe-js';

@Injectable({
  providedIn: 'root'
})
export class StripeService {
    private stripe: Stripe | null = null;

    async loadStripe() {
      if (!this.stripe) {
        this.stripe = await loadStripe('pk_test_51Qb5FvEKQfVedogdaMc6uhVFk7DpZVk2AoZK2bFAvRZZJttUC1u1U9kpVQFJPNTEJUxULR7n2wIZDtaqmzBieqwq00kQ64alRn');
      }
      return this.stripe;
    }
  
    createCardElement(): StripeCardElement {
      if (!this.stripe) throw new Error('Stripe not loaded');
      const elements = this.stripe.elements();
      return elements.create('card',{
        style: {
            base: {
              fontSize: '16px',
              color: '#32325d',
              fontFamily: '"Helvetica Neue", Helvetica, sans-serif',
              fontSmoothing: 'antialiased',
              '::placeholder': {
                color: '#aab7c4'
              }
            },
            invalid: {
              color: '#fa755a',
              iconColor: '#fa755a'
            }
          },
          hidePostalCode: true // This removes the ZIP code field
      });
    }
  
    async createToken(cardElement: StripeCardElement) {
      if (!this.stripe) throw new Error('Stripe not loaded');
      return this.stripe.createToken(cardElement);
    }
}

