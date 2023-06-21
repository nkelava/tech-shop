<script setup>
import { RouterLink } from "vue-router";
import ContactInfo from "@/components/common/ContactInfo.vue";
import EmailIcon from "@/assets/icons/contact/gmail16.png";
import PhoneIcon from "@/assets/icons/contact/phone16.png";
import FacebookIcon from "@/assets/icons/socials/facebook.png";
import InstagramIcon from "@/assets/icons/socials/instagram.png";
import YoutubeIcon from "@/assets/icons/socials/youtube.png";

const categories = ["laptops", "computers", "components", "peripherals", "software"];
</script>

<template>
  <!-- TODO: fix footer position (not sticking to bottom) -->
  <div class="footer">
    <div class="footer__content">
      <!-- TODO: maybe add one more section -->
      <div class="contact-container">
        <h3 class="footer__header">CONTACT US</h3>
        <form class="contact-form">
          <input type="text" placeholder="Enter email..." required />
          <input type="text" placeholder="Enter subject..." required />
          <!-- TODO: fix textarea resizing min and max -->
          <textarea cols="50" rows="10" placeholder="Enter message here..." required />
          <input type="submit" class="contact-form__btn" value="Send" />
        </form>
      </div>
      <div class="categories-container">
        <h3 class="footer__header">CATEGORIES</h3>
        <router-link v-for="(category, index) in categories" :key="index" :to="category">
          {{ category }}
        </router-link>
      </div>
      <div class="info-container">
        <h3 class="footer__header">CONTACT INFO</h3>
        <contact-info :imgUrl="EmailIcon" imgAlt="email icon">
          Email: tech-store@gmail.com
        </contact-info>
        <v-divider class="my-1" />
        <contact-info :imgUrl="PhoneIcon" imgAlt="phone icon"> Phone: 123-456-7890 </contact-info>
        <div class="info__socials">
          <img :src="FacebookIcon" class="socials__icon" title="Facebook" alt="facebook icon" />
          <img :src="InstagramIcon" class="socials__icon" title="Instagram" alt="instagram icon" />
          <img :src="YoutubeIcon" class="socials__icon" title="Youtube" alt="youtube icon" />
        </div>
      </div>
    </div>
    <div class="copyright">
      <h4>Copyright &copy; 2023 TechShop</h4>
    </div>
  </div>
</template>

<style scoped>
.footer {
  padding: 2rem 5rem;
}

.footer__content {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-around;
  gap: 2rem;
}

.footer__header {
  color: var(--ts-c-text-highlight);
  margin-bottom: 1rem;
}

.contact-form {
  max-width: 300px;
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
  position: relative;
  padding-right: 1rem;
}

.contact-form > * {
  background-color: var(--ts-c-bg-light);
  border: none;
  border-radius: 5px;
  color: var(--ts-c-text-dark);
  padding: 3px 5px;
}

.contact-form__btn {
  background: none;
  position: absolute;
  bottom: 5px;
  right: 1.5rem;
}

.categories-container {
  display: flex;
  flex-direction: column;
}

.categories-container a {
  color: var(--ts-c-text-light);
  text-transform: capitalize;
  padding-bottom: 5px;
}

.info-container {
  display: flex;
  flex-direction: column;
}

img {
  margin-right: 10px;
}

.info__socials {
  padding-top: 2rem;
  display: flex;
}

.info__socials > * {
  padding-right: 10px;
}

.copyright {
  color: var(--ts-c-primary-mute);
  border-top: 1px solid var(--ts-c-primary-dark);
  margin-top: 1rem;
  padding: 5px;
  text-align: center;
}
</style>
