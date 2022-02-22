<template>
  <div class="modal px-3" :class="{ 'is-active': isActive }">
    <div class="modal-background"></div>
    <div class="modal-card">
      <header class="modal-card-head">
        <p class="modal-card-title">{{ props.title }}</p>
        <button class="delete" aria-label="close" @click="props.closeModal!()"></button>
      </header>
      <section class="modal-card-body">
        <slot></slot>
      </section>
      <footer class="modal-card-foot">
        <button class="button" @click="props.closeModal!()">Close</button>
      </footer>
    </div>
  </div>
</template>
<script setup lang="ts">
const props = defineProps({
  title: String,
  isActive: Boolean,
  closeModal: Function,
});
</script>
<style lang="scss"></style>
