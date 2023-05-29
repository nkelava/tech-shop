<script setup>
import TheHeader from "@/components/TheHeader.vue";
import TheNewsletters from "@/components/TheNewsletters.vue";
import TheFooter from "@/components/TheFooter.vue";
</script>

<template>
  <div class="baselayout">
    <TheHeader class="baselayout__header" />
    <main class="baselayout__main">
      <slot />
    </main>
    <TheNewsletters class="baselayout__newsletters" />
    <TheFooter class="baselayout__footer" />
  </div>
</template>

<style scoped>
.baselayout__main {
  min-height: calc(100vh - 128px - 498px);
}
</style>
