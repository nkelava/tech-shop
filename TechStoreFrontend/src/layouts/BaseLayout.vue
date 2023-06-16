<script setup>
import TheHeader from "@/components/common/header/TheHeader.vue";
import TheNewsletters from "@/components/common/newsletters/TheNewsletters.vue";
import TheFooter from "@/components/common/footer/TheFooter.vue";
</script>

<template>
  <div class="baselayout">
    <the-header class="baselayout__header" />
    <main class="baselayout__main">
      <slot />
    </main>
    <the-newsletters class="baselayout__newsletters" />
    <the-footer class="baselayout__footer" />
  </div>
</template>

<style scoped>
.baselayout__main {
  min-height: calc(100vh - 128px - 498px);
}
</style>
