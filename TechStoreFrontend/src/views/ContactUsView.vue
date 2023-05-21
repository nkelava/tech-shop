<script setup>
import ContactImage from "@/assets/images/test/contact/contact_us.png";
import EmailIcon from "@/assets/icons/contact/gmail_m.png";
import PhoneIcon from "@/assets/icons/contact/phone_m.png";
</script>

<template>
  <section id="contact">
    <div classk="contact__content">
      <img :src="ContactImage" class="contact__image" alt="" />
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
      <h1>Contact Us</h1>
      <form class="form">
        <input type="text" placeholder="Enter your name..." />
        <input type="email" placeholder="Enter your email..." />
        <input type="tel" name="" id="" placeholder="Enter your phone number..." />
        <textarea cols="50" rows="10" placeholder="Enter your message..." />
      </form>
    </div>
  </section>
</template>

<style scoped>
#contact {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: 1rem;
  margin: 5rem 0;
  width: 100%;
  height: 100%;
}

.contact__image {
  height: auto;
  max-width: 100%;
}

.contact__info {
  display: flex;
  justify-content: space-evenly;
  gap: 2rem;
  margin-top: 1rem;
}

.info__item {
  display: flex;
  align-items: center;
  gap: 1rem;
  font-size: 16px;
}

.contact__form {
  text-align: center;
}

.contact__form h1 {
  letter-spacing: 2px;
  margin-bottom: 3rem;
}

.form {
  display: flex;
  flex-direction: column;
  gap: 2rem;
  height: 100%;
  width: 100%;
}

.form > * {
  background: none;
  border-radius: 5px;
  color: var(--ts-c-text-light);
  padding: 5px 10px;
  font-size: 1rem;
}

.form input {
  border: none;
  border-bottom: 1px solid var(--ts-c-bg-light);
}

.form textarea {
  border: 1px solid var(--ts-c-bg-light);
}
</style>
