<script setup>
import ContactForm from "@/components/ContactForm.vue";
import { ContactUsImage } from "@/assets/images/test/contact";
import EmailIcon from "@/assets/icons/contact/gmail32.png";
import PhoneIcon from "@/assets/icons/contact/phone32.png";
</script>

<template>
  <div class="contact-container">
    <section id="contact">
      <div class="contact__content">
        <img :src="ContactUsImage" class="contact__image" alt="" />
        <div class="contact__info">
          <div class="info__item">
            <img :src="EmailIcon" class="info__icon" alt="email icon" />
            <div>
              <p>Email:</p>
              <p>techshop@gmail.com</p>
            </div>
          </div>
          <div class="info__item">
            <img :src="PhoneIcon" class="info__icon" alt="phone icon" />
            <div>
              <p>Phone:</p>
              <p>123-456-7890</p>
            </div>
          </div>
        </div>
      </div>
      <div class="contact__form">
        <h1 class="form__title">Contact Us</h1>
        <contact-form />
      </div>
    </section>
  </div>
</template>

<style scoped>
.contact-container {
  padding: 2rem 5rem;
}

#contact {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: 5rem;
  margin: 5rem 0;
}

.contact__content {
  min-width: 300px;
  max-width: 600px;
}

.contact__image {
  height: auto;
  width: 100%;
}

.contact__info {
  display: flex;
  justify-content: space-around;
  gap: 2rem;
  margin-top: 2rem;
}

.info__item {
  display: flex;
  align-items: center;
  gap: 1rem;
  font-size: 1rem;
}

.form__title {
  color: var(--ts-c-ternary);
  font-size: 1.5rem;
  letter-spacing: 2px;
  margin-bottom: 1rem;
}

@media only screen and (max-width: 450px) {
  .contact__content {
    display: none;
  }

  .contact__form {
    min-width: 250px;
  }
}
</style>
